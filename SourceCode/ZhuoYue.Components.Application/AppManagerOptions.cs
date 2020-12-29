using ZhuoYue.Components.Cache.Abstractions;

namespace ZhuoYue.Components.Application
{
    public class AppManagerOptions
    {
        public bool EnableCache { get; set; } = true;

        public ICacheManager CacheManager { get; internal set; }

    }
}