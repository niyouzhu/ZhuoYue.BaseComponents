using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ZhuoYue.Components.Application.Abstractions;
using ZhuoYue.Components.Application.EntityFrameworkCore;

namespace ZhuoYue.Components.Application.NTest
{
    public class Tests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            using (var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new AppManager(new AppProvider(dbContext), new AppManagerOptions() { EnableCache = false });
                var all = manager.ReadAll();
                manager.Delete(all);
                TestCreations();
            }

        }


        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZhuoYue_Components_Dev; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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

        [Test]
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
                searchCritiera.AppIds.Add(rt.AppId);
                var app2 = manager.ReadOne(searchCritiera);
                Assert.True(rt.AppId == app2.AppId);
                manager.Delete(app2);
                apps = manager.ReadAll();
                manager.Delete(apps);
            }

        }
        [Test]
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

        [Test]

        public void TestReadingWithoutCache()
        {
            App app;
            AppManager manager;
            using (var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options))
            {
                manager = new AppManager(new AppProvider(dbContext), new AppManagerOptions() { EnableCache = false });
                var searchCriteria = new SearchCriteria();
                searchCriteria.OrderBy.Add((nameof(App.AppId), Core.AscOrDesc.Desc));
                searchCriteria.OrderBy.Add((nameof(App.CreatedTime), Core.AscOrDesc.Asc));
                searchCriteria.OrderBy.Add((nameof(App.AppRemarks), null));
                searchCriteria.OrderBy.Add((nameof(App.CreatedUserId), Core.AscOrDesc.Desc));
                searchCriteria.PageIndex = 2;
                searchCriteria.PageSize = 23;
                var searchResult = manager.Read(searchCriteria);
                Assert.True(searchResult.Count() == 23);
                searchResult = manager.ReadAll();
                Assert.True(searchResult.Count() > 0);
                var first = searchResult.First();
                searchCriteria = new SearchCriteria();
                searchCriteria.AppIds.Add(first.AppId);
                app = manager.ReadOne(searchCriteria);
                Assert.True(app.AppId == first.AppId);
                searchCriteria.AppIds.Add(searchResult.ElementAt(1).AppId);
                var apps = manager.Read(searchCriteria);
                Assert.True(apps.Count() == 2);
                searchCriteria.AppIds.Clear();
                searchCriteria.AppNames.Add(first.AppName);
                searchCriteria.AppNames.Add(searchResult.ElementAt(1).AppName);
                apps = manager.Read(searchCriteria);
                Assert.True(apps.Count() > 0);

            }
            Assert.Throws<ObjectDisposedException>(() => manager.ReadAll().First());
        }
        [Test]
        public void TestReadingWithCache()
        {
            App app;
            App app2;
            AppManager manager;

            using (var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var provider = new AppProvider(dbContext);
                manager = new AppManager(provider, new AppManagerOptions() { EnableCache = true });
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
                searchCriteria.AppIds.Add(first.AppId);
                app = manager.ReadOne(searchCriteria);
                Assert.True(app.AppId == first.AppId);
                searchCriteria.AppIds.Add(rt.ElementAt(1).AppId);
                var apps = manager.Read(searchCriteria);
                Assert.True(apps.Count() == 2);
                searchCriteria.AppIds.Clear();
                searchCriteria.AppNames.Add(first.AppName);
                searchCriteria.AppNames.Add(rt.ElementAt(1).AppName);
                apps = manager.Read(searchCriteria);
                Assert.True(apps.Count() >= 2);
            }
            {
                var first = manager.ReadAll().First();
                var searchCriteria = new SearchCriteria();
                searchCriteria.AppIds.Add(first.AppId);
                app2 = manager.ReadOne(searchCriteria);
                Assert.True(app.AppId == app2.AppId);
                Assert.True(app == app2);
            }
        }

    }
}