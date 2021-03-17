using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuoYue.Components.Core
{
    public class StringHelper
    {
        public static string Join(string separator, params object[] objects)
        {
            var rt = new List<object>();
            for (int i = 0; i < objects.Length; i++)
            {
                var asEnumerable = objects[i] as IEnumerable;
                var asCovnerted = new List<string>();
                if (asEnumerable != default)
                {
                    foreach (var item in asEnumerable)
                    {
                        if (item != default)
                        {
                            var asString = item.ToString();
                            asCovnerted.Add(asString);
                        }
                    }
                    if (asCovnerted.Count != 0)
                        rt.Add(string.Join(separator, asCovnerted));
                }
                else
                {
                    if (objects[i] != default)
                        rt.Add(objects[i]);
                }
            }
            if (rt.Count == 0) return null;
            return string.Join(separator, rt);
        }

        public static string[] AddIfNotExisting(string[] source, params object[] items)
        {
            List<string> rt;
            if (source != null)
                rt = new List<string>(source);
            else
                rt = new List<string>();
            foreach (var item in items)
            {

                if (null != item)
                {
                    if (item is IEnumerable && !(item is string))
                    {
                        foreach (var child in (IEnumerable)item)
                        {
                            var toString = child.ToString();
                            if (!string.IsNullOrWhiteSpace(toString) && !rt.ContainsInsensitive(toString))
                            {
                                rt.Add(toString);
                            }
                        }
                    }
                    else
                    {
                        var toString = item.ToString();
                        if (!string.IsNullOrWhiteSpace(toString) && !rt.ContainsInsensitive(toString))
                        {
                            rt.Add(toString);
                        }
                    }

                }
            }
            return rt.ToArray();
        }

        public static string JoinRemoveNullEmpty(string separator, params object[] items)
        {
            if (items == null) return null;
            var rt = new List<object>();
            foreach (var item in items)
            {

                if (null != item)
                {
                    if (item is IEnumerable && !(item is string))
                    {
                        foreach (var child in (IEnumerable)item)
                        {
                            var toString = child.ToString();
                            if (!string.IsNullOrWhiteSpace(toString))
                            {
                                rt.Add(child);
                            }
                        }
                    }
                    else
                    {
                        var toString = item.ToString();
                        if (!string.IsNullOrWhiteSpace(toString))
                        {
                            rt.Add(item);
                        }
                    }

                }
            }
            if (rt.Count() == 0) return null;
            return string.Join(separator, rt.ToArray());

        }
    }
}
