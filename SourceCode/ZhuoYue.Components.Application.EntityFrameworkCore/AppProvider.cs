using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuoYue.Components.Application.Abstractions;

namespace ZhuoYue.Components.Application.EntityFrameworkCore
{
    public class AppProvider : IAppProvider
    {
        public AppDbContext DbContext { get; }
        public AppProvider(AppDbContext context)
        {
            DbContext = context;
        }

        public string ProviderName { get; set; } = "AppProvider.EntityFrameworkCore";
        public App Create(App app)
        {
            var rt = new AppEntity() { AppId = Guid.NewGuid().ToString(), AppName = app.AppName, AppRemarks = app.AppRemarks, CreatedUserId = app.CreatedUserId };
            DbContext.Apps.Add(rt);
            DbContext.SaveChanges();
            return rt;
        }

        public IEnumerable<App> Create(IEnumerable<App> apps)
        {
            var rt = new List<AppEntity>(apps.Count());
            apps.ForEach(it => { rt.Add(new AppEntity() { AppId = Guid.NewGuid().ToString(), AppName = it.AppName, AppRemarks = it.AppRemarks, CreatedUserId = it.CreatedUserId }); });
            DbContext.Apps.AddRange(rt);
            DbContext.SaveChanges();
            return rt;
        }


        public App Delete(App app)
        {
            var entity = DbContext.Apps.Find(app.AppId);
            DbContext.Remove(entity);
            DbContext.SaveChanges();
            return app;
        }

        public IEnumerable<App> Delete(IEnumerable<App> apps)
        {
            var ids = apps.Select(it => it.AppId);
            var rt = DbContext.Apps.Where(it => ids.Contains(it.AppId)).ToList();
            DbContext.Apps.RemoveRange(rt);
            DbContext.SaveChanges();
            return rt;
        }

        public IEnumerable<App> Read(SearchCriteria searchCriteria)
        {
            IQueryable<App> queryable = DbContext.Apps;
            if (searchCriteria.AppIds.Any())
            {
                queryable = queryable.Where(it => searchCriteria.AppIds.Contains(it.AppId));
            }
            if (searchCriteria.AppNames.Any())
            {
                queryable = queryable.Where(it => searchCriteria.AppNames.Contains(it.AppName));
            }
            if (searchCriteria.OrderBy.Any())
                queryable = queryable.OrderBy(searchCriteria.OrderBy).AsQueryable();
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
            var rt = DbContext.Apps.Find(app.AppId);
            rt.AppName = app.AppName;
            rt.AppRemarks = app.AppRemarks;
            rt.LastUpdatedTime = DateTime.Now;
            rt.LastUpdatedUserId = app.LastUpdatedUserId;
            DbContext.SaveChanges();
            return rt;
        }

        public IEnumerable<App> Update(IEnumerable<App> apps)
        {
            var ids = apps.Select(it => it.AppId);
            var rt = DbContext.Apps.Where(it => ids.Contains(it.AppId));
            rt.ForEach(it =>
            {

                var first = apps.FirstOrDefault(_ => _.AppId == it.AppId);
                if (first != null)
                {
                    it.AppName = first.AppName;
                    it.AppRemarks = first.AppRemarks;
                    it.LastUpdatedTime = first.LastUpdatedTime;
                    it.LastUpdatedUserId = first.LastUpdatedUserId;
                }
            });
            DbContext.SaveChanges();
            return rt;
        }
    }
}
