using System;

namespace ZhuoYue.Components.Cache.Abstractions
{
    public interface ICacheManager
    {
        object Create(object key, object obj);

        object Read(object key);

        object Update(object key, object obj);

        void Delete(object key);

        bool Exists(object key);

        void Clear();
    }
}
