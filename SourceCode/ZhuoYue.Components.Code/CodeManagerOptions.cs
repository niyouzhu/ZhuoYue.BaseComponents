using ZhuoYue.Components.Cache.Abstractions;

namespace ZhuoYue.Components.Code
{
    public class CodeManagerOptions
    {
        public bool EnableCache { get; set; } = true;

        public ICacheManager CacheManager { get; }
    }
}