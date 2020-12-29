using System;
using System.Collections.Generic;
using System.Linq;
using ZhuoYue.Components.Application.Abstractions;
using ZhuoYue.Components.Cache;
using ZhuoYue.Components.Cache.LocalMemory;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Application
{
    public class AppManager : IAppManager
    {
        public AppManager(IAppProvider appProvider, AppManagerOptions options)
        {
            Options = options;
            if (appProvider is Cache.AppProvider)
            {
                throw new BusinessException($"Cannot direct to use {nameof(Cache.AppProvider)}!");

            }
            if (Options.EnableCache)
            {
                if (Options.CacheManager == null)
                    Options.CacheManager = new CacheManager(new CacheProvider(), new CacheManagerOptions() { });
                Provider = new Cache.AppProvider(appProvider, Options.CacheManager);
            }
            else
            {
                Provider = appProvider;
            }

        }

        public IAppProvider Provider { get; }
        public AppManagerOptions Options { get; }

        public App Create(App app)
        {
            var rt = Provider.Create(app);
            return rt;
        }

        public IEnumerable<App> Create(IEnumerable<App> apps)
        {
            var rt = Provider.Create(apps);
            return rt;
        }

        public IEnumerable<App> Read(SearchCriteria searchCriteria)
        {
            return Provider.Read(searchCriteria);
        }

        public App ReadOne(SearchCriteria searchCriteria)
        {
            var rt = Read(searchCriteria);
            if (rt.Count() > 1)
            {
                throw new BusinessException($"There is more than 1 result based on the search criteria. ");
            }
            return rt.First();
        }

        public IEnumerable<App> ReadAll()
        {
            return Read(new SearchCriteria() { PageSize = int.MaxValue });
        }


        public App Update(App app)
        {
            var rt = Provider.Update(app);
            return rt;
        }

        public IEnumerable<App> Update(IEnumerable<App> apps)
        {
            var rt = Provider.Update(apps);
            return rt;
        }

        public App Delete(App app)
        {
            var rt = Provider.Delete(app);
            return rt;
        }

        public IEnumerable<App> Delete(IEnumerable<App> apps)
        {
            var rt = Provider.Delete(apps);
            return rt;
        }
    }
}
