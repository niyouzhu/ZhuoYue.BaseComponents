using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ZhuoYue.Components.Application;
using ZhuoYue.Components.Application.Abstractions;
using ZhuoYue.Components.Application.EntityFrameworkCore;

namespace ZhuoYue.Components.Application.Test
{
    public class UnitTest1
    {

        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZhuoYue_Components_Dev; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        [Fact]
        public void TestCreation()
        {
            using (var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new AppManager(new AppProvider(dbContext), new AppManagerOptions() { EnableCache = false });
                var app = new App() { AppName = "Foo", AppRemarks = "FooRemarks", CreatedUserId = "EN" };
                var rt = manager.Create(app);
                Assert.True(app != rt);
                Assert.True(!string.IsNullOrWhiteSpace(rt.AppId));
            }

        }

        [Fact]
        public void TestDeletionWithCache()
        {
            using (var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new AppManager(new AppProvider(dbContext), new AppManagerOptions() { EnableCache = true });
                var app = new App() { AppName = "Foo", AppRemarks = "FooRemarks", CreatedUserId = "EN" };
                var rt = manager.Create(app);
                Assert.True(app != rt);
                Assert.True(!string.IsNullOrWhiteSpace(rt.AppId));
                var apps = manager.ReadAll();
                var searchCritiera = new SearchCriteria();
                searchCritiera.AppId.Add(rt.AppId);
                var app2 = manager.ReadOne(searchCritiera);
                Assert.True(rt.AppId == app2.AppId);
            }

        }
        [Fact]
        public void TestCreations()
        {
            using (var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new AppManager(new AppProvider(dbContext), new AppManagerOptions() { EnableCache = false });
                var apps = new List<App>();
                for (int i = 0; i < 100; i++)
                {
                    apps.Add(new App() { AppName = $"Foo{i}", AppRemarks = $"FooRemarks{i}", CreatedUserId = $"EN{i}" });
                }
                var rt = manager.Create(apps);
                Assert.True(rt != apps);
                Assert.True(rt.Count() == 100);
            }
        }

        [Fact]
        public void TestReadingWithoutCache()
        {
            using (var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new AppManager(new AppProvider(dbContext), new AppManagerOptions() { EnableCache = false });
                var searchCriteria = new SearchCriteria();
                searchCriteria.OrderBy.Add((nameof(App.AppId), Core.AscOrDesc.Desc));
                searchCriteria.OrderBy.Add((nameof(App.CreatedTime), Core.AscOrDesc.Asc));
                searchCriteria.OrderBy.Add((nameof(App.AppRemarks), null));
                searchCriteria.OrderBy.Add((nameof(App.CreatedUserId), Core.AscOrDesc.Desc));
                searchCriteria.PageIndex = 2;
                searchCriteria.PageSize = 23;
                var rt = manager.Read(searchCriteria);
                Assert.True(rt.Count() == 23);
                rt = manager.ReadAll();
                Assert.True(rt.Count() > 0);
                var first = rt.First();
                searchCriteria = new SearchCriteria();
                searchCriteria.AppId.Add(first.AppId);
                var app = manager.ReadOne(searchCriteria);
                Assert.True(app.AppId == first.AppId);
                searchCriteria.AppId.Add(rt.ElementAt(1).AppId);
                var apps = manager.Read(searchCriteria);
                Assert.True(apps.Count() == 2);
                searchCriteria.AppId.Clear();
                searchCriteria.AppName.Add(first.AppName);
                searchCriteria.AppName.Add(rt.ElementAt(1).AppName);
                apps = manager.Read(searchCriteria);
                Assert.True(apps.Count() > 0);

            }
        }
        [Fact]
        public void TestReadingWithCache()
        {
            using (var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new AppManager(new AppProvider(dbContext), new AppManagerOptions() { EnableCache = true });
                var searchCriteria = new SearchCriteria();
                searchCriteria.OrderBy.Add((nameof(App.AppId), Core.AscOrDesc.Desc));
                searchCriteria.OrderBy.Add((nameof(App.CreatedTime), Core.AscOrDesc.Asc));
                searchCriteria.OrderBy.Add((nameof(App.AppRemarks), null));
                searchCriteria.OrderBy.Add((nameof(App.CreatedUserId), Core.AscOrDesc.Desc));
                searchCriteria.PageIndex = 2;
                searchCriteria.PageSize = 23;
                var rt = manager.Read(searchCriteria);
                Assert.True(rt.Count() == 23);
                rt = manager.ReadAll();
                Assert.True(rt.Count() > 0);
                var first = rt.First();
                searchCriteria = new SearchCriteria();
                searchCriteria.AppId.Add(first.AppId);
                var app = manager.ReadOne(searchCriteria);
                Assert.True(app.AppId == first.AppId);
                searchCriteria.AppId.Add(rt.ElementAt(1).AppId);
                var apps = manager.Read(searchCriteria);
                Assert.True(apps.Count() == 2);
                searchCriteria.AppId.Clear();
                searchCriteria.AppName.Add(first.AppName);
                searchCriteria.AppName.Add(rt.ElementAt(1).AppName);
                apps = manager.Read(searchCriteria);
                Assert.True(apps.Count() >= 2);
            }
        }
    }
}
