using System;
using System.Collections.Generic;
using System.Linq;
using ZhuoYue.Components.Application.Abstractions;
using ZhuoYue.Components.Cache.Abstractions;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Application.Cache
{

    public class AppProvider : IAppProvider
    {
        public IAppProvider InternalProvider { get; }
        public ICacheManager CacheManager { get; }

        public string ProviderName { get; set; } = "AppProvider.Cache";

        public AppProvider(IAppProvider internalProvider, ICacheManager cacheManager)
        {
            InternalProvider = internalProvider;
            CacheManager = cacheManager;
        }

        public App Create(App app)
        {
            return InternalProvider.Create(app);
        }

        public IEnumerable<App> Create(IEnumerable<App> apps)
        {
            var rt = InternalProvider.Create(apps);
            CacheManager.Delete(CachePrefix.App);
            return rt;
        }

        public App Delete(App app)
        {
            var rt = InternalProvider.Delete(app);
            CacheManager.Delete(CachePrefix.App);
            return rt;

        }

        public IEnumerable<App> Delete(IEnumerable<App> apps)
        {
            var rt = InternalProvider.Delete(apps);
            CacheManager.Delete(CachePrefix.App);
            return rt;
        }

        public IEnumerable<App> Read(SearchCriteria searchCriteria)
        {
            IEnumerable<App> all;
            if (CacheManager.Exists(CachePrefix.App))
            {
                all = (IEnumerable<App>)CacheManager.Read(CachePrefix.App);
            }
            else
            {
                all = InternalProvider.Read(new SearchCriteria() { PageSize = null });
                all = (IEnumerable<App>)CacheManager.Create(CachePrefix.App, all);
            }
            var queryable = all;

            if (searchCriteria.AppId.Any())
            {
                queryable = queryable.Where(it => searchCriteria.AppId.Contains(it.AppId));
            }
            if (searchCriteria.AppName.Any())
            {
                queryable = queryable.Where(it => searchCriteria.AppName.Contains(it.AppName));
            }
            if (searchCriteria.OrderBy.Any())
                queryable = queryable.OrderBy(searchCriteria.OrderBy);
            if (!searchCriteria.PageSize.HasValue || searchCriteria.PageSize.Value == int.MaxValue)
            {
                return queryable.ToList();
            }
            if (searchCriteria.PageIndex > 0)
            {
                queryable = queryable.Skip(searchCriteria.PageSize.Value * searchCriteria.PageIndex).Take(searchCriteria.PageSize.Value);
            }
            else
            {
                queryable = queryable.Take(searchCriteria.PageSize.Value);
            }
            return queryable.ToList();
        }

        public App Update(App app)
        {
            var rt = InternalProvider.Update(app);
            CacheManager.Delete(CachePrefix.App);
            return rt;
        }

        public IEnumerable<App> Update(IEnumerable<App> apps)
        {
            var rt = InternalProvider.Update(apps);
            CacheManager.Delete(CachePrefix.App);
            return rt;
        }
    }
}
