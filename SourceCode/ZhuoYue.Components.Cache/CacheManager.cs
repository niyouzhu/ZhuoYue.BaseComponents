using System;
using ZhuoYue.Components.Cache.Abstractions;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Cache
{
    public class CacheManager : ICacheManager
    {
        public CacheManager(ICacheProvider provider, CacheManagerOptions options)
        {
            Provider = provider;
            Options = options;
        }

        public ICacheProvider Provider { get; }

        public CacheManagerOptions Options { get; }

        public void Clear()
        {
            Provider.Clear();
        }

        public object Create(object key, object obj)
        {
            return Provider.Create(key, obj);
        }

        public void Delete(object key)
        {
            Provider.Delete(key);
        }

        public bool Exists(object key)
        {
            return Provider.Exists(key);
        }

        public object Read(object key)
        {
            return Provider.Read(key);
        }

        public object Update(object key, object obj)
        {
            return Provider.Update(key, obj);
        }
    }
}
