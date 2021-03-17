using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public static TSource AddRange<TSource, TRange, TItem>(this TSource source, TRange range) where TSource : ICollection<TItem> where TRange : IEnumerable<TItem> where TItem : class
        {
            foreach (var item in range)
            {
                source.Add(item);
            }
            return source;
        }

        public static T ForEach<T, TItem>(this T source, Action<TItem> action) where T : IEnumerable<TItem>
        {
            foreach (var item in source)
            {
                action(item);
            }
            return source;
        }
    }
}

namespace System.Collections.Generic
{

}
namespace System.IO
{
    public static class Extensions
    {

    }
}

namespace System.Data
{
    public static class Extensions
    {
        public static decimal? GetDecimalNull(this IDataReader reader, string fieldName)
        {
            var index = reader.GetOrdinal(fieldName);
            var value = reader.GetValue(index);
            if (value is DBNull) return null;
            return (decimal)value;
        }

        public static short? GetShortNull(this IDataReader reader, string fieldName)
        {
            var index = reader.GetOrdinal(fieldName);
            var value = reader.GetValue(index);
            if (value is DBNull) return null;
            return (short)value;
        }
        public static string GetStringNull(this IDataReader reader, string fieldName)
        {
            var index = reader.GetOrdinal(fieldName);
            var value = reader.GetValue(index);
            if (value is DBNull) return null;
            return (string)value;
        }

        public static int? GetInt32Null(this IDataReader reader, string fieldName)
        {
            var index = reader.GetOrdinal(fieldName);
            var value = reader.GetValue(index);
            if (value is DBNull) return null;
            return (int)value;
        }
    }
}

namespace System
{
    public static class Extensions
    {
        public static T? TryParseEnum<T>(this string value) where T : struct
        {

            if (Enum.TryParse<T>(value, true, out var rt))
            {
                return rt;
            }
            return null;
        }

        private static object GetPropertyValueInternal(object obj, string prorpertyName, object[] index = null)
        {
            var property = obj.GetType().GetProperty(prorpertyName);
            if (property != null)
            {
                return property.GetValue(obj, index);
            }
            throw new ArgumentException($"The property {prorpertyName} is not existed.");
        }
        private static object GetPropertyValue(object obj, IEnumerable<string> propertiesPath, object[] index = null)
        {
            for (int i = 0; i < propertiesPath.Count(); i++)
            {
                obj = GetPropertyValueInternal(obj, propertiesPath.ElementAt(i), index);
            }
            return obj;
        }
        public static object GetPropertyValue(this object obj, string propertyPath, object[] index = null)
        {
            var propertiesPath = propertyPath?.Trim()?.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            return GetPropertyValue(obj, propertiesPath, index);
        }
        public static bool ContainsInsensitive(this string source, string value)
        {
            return source.ToUpper().Contains(value.ToUpper());
        }

        public static bool ContainsInsensitive(this IEnumerable<string> source, string value)
        {
            foreach (var item in source)
            {
                var contained = string.Equals(item, value, StringComparison.InvariantCultureIgnoreCase);
                if (contained) return true;
            }
            return false;
        }

        public static bool ContainsInsensitive(this string source, IEnumerable<string> value)
        {
            foreach (var item in value)
            {
                var contained = source.ContainsInsensitive(item);
                if (contained) return true;
            }
            return false;
        }

        public static DateTime? ParseAsDateTime(this string datetimeString, string format)
        {
            DateTime dateTime;
            if (!string.IsNullOrWhiteSpace(datetimeString) && DateTime.TryParseExact(datetimeString, format, CultureInfo.CurrentCulture, DateTimeStyles.None | DateTimeStyles.AllowWhiteSpaces, out dateTime))
            {
                return dateTime;
            }
            return null;
        }

        public static int? ParseAsInt(this string idString)
        {
            int rt;
            if (!string.IsNullOrWhiteSpace(idString) && int.TryParse(idString, out rt))
            {
                return rt;
            }
            return null;
        }

        public static double? ParseAsDouble(this string idString)
        {
            double rt;
            if (!string.IsNullOrWhiteSpace(idString) && double.TryParse(idString, out rt))
            {
                return rt;
            }
            return null;
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
