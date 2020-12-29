using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ZhuoYue.Components.Core;

namespace System.Collections.Generic
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static IEnumerable<T> AddRange<T>(this ICollection<T> source, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                source.Add(item);
            }
            return source;
        }

    }
}

namespace System.Linq
{
    public static class Extensions
    {

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, IEnumerable<ValueTuple<string, AscOrDesc?>> orderby)
        {
            IEnumerable<T> rt = source.AsQueryable();
            IEnumerable<T> orderedQueryable = null;
            var type = typeof(T);
            var queryableType = typeof(Queryable);
            var orderyMethod = queryableType.GetMethods().First(it => it.Name == "OrderBy" && it.GetParameters().Length == 2);
            var orderByDescendingMethod = queryableType.GetMethods().First(it => it.Name == "OrderByDescending" && it.GetParameters().Length == 2);
            var thenByMethod = queryableType.GetMethods().First(it => it.Name == "ThenBy" && it.GetParameters().Length == 2);
            var thenByDescendingMethod = queryableType.GetMethods().First(it => it.Name == "ThenByDescending" && it.GetParameters().Length == 2);

            var parameter = Expression.Parameter(type, "SourceType");
            for (int i = 0; i < orderby.Count(); i++)
            {
                var property = Expression.Property(parameter, orderby.ElementAt(i).Item1);
                var lambda = Expression.Lambda(property, parameter);

                if (i == 0)
                {
                    if (orderby.ElementAt(i).Item2.HasValue && orderby.ElementAt(i).Item2 == AscOrDesc.Desc)
                    {
                        var genericMethod = orderByDescendingMethod.MakeGenericMethod(typeof(T), property.Type);
                        orderedQueryable = (IEnumerable<T>)genericMethod.Invoke(null, new object[] { rt, lambda });
                    }
                    else
                    {
                        var genericMethod = orderyMethod.MakeGenericMethod(typeof(T), property.Type);
                        orderedQueryable = (IEnumerable<T>)genericMethod.Invoke(null, new object[] { rt, lambda });

                    }
                }
                else
                {
                    if (orderby.ElementAt(i).Item2.HasValue && orderby.ElementAt(i).Item2 == AscOrDesc.Desc)
                    {
                        var genericMethod = thenByDescendingMethod.MakeGenericMethod(typeof(T), property.Type);
                        orderedQueryable = (IEnumerable<T>)genericMethod.Invoke(null, new object[] { orderedQueryable, lambda });
                    }
                    else
                    {
                        var genericMethod = thenByMethod.MakeGenericMethod(typeof(T), property.Type);
                        orderedQueryable = (IEnumerable<T>)genericMethod.Invoke(null, new object[] { orderedQueryable, lambda });
                    }
                }
            }
            if (orderedQueryable == null)
                return rt;
            return orderedQueryable;
        }
    }
}
