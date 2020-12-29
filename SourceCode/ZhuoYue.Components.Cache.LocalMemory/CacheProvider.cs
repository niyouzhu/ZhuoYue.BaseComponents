using System;
using System.Collections;
using ZhuoYue.Components.Cache.Abstractions;
using System.Linq;

namespace ZhuoYue.Components.Cache.LocalMemory
{
    public class CacheProvider : ICacheProvider
    {
        public Hashtable CacheStore { get; } = new Hashtable();

        public string ProviderName { get; set; } = "CacheProvider.LocalMemory";

        public void Clear()
        {
            CacheStore.Clear();
        }

        public object Create(object key, object obj)
        {
            CacheStore.Add(key, obj);
            return obj;
        }

        public void Delete(object key)
        {
            CacheStore.Remove(key);
        }

        public bool Exists(object key)
        {
            return CacheStore.Contains(key);
        }

        public object Read(object key)
        {
            return CacheStore[key];
        }

        public object Update(object key, object obj)
        {
            CacheStore[key] = obj;
            return obj;
        }
    }
}
