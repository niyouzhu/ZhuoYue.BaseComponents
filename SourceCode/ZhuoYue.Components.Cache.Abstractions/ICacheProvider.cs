using System;
using System.Collections.Generic;
using System.Text;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Cache.Abstractions
{
    public interface ICacheProvider : IProvider
    {
        string ProviderName { get; set; }
        object Create(object key, object obj);

        object Read(object key);

        object Update(object key, object obj);

        void Delete(object key);

        bool Exists(object key);

        void Clear();
    }
}
