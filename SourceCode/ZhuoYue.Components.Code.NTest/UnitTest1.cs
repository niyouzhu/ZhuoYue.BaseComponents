using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ZhuoYue.Components.Application;
using ZhuoYue.Components.Application.Abstractions;
using ZhuoYue.Components.Application.EntityFrameworkCore;
using ZhuoYue.Components.Code.Abstractions;
using ZhuoYue.Components.Code.EntityFrameworkCore;
using SearchCriteria = ZhuoYue.Components.Code.Abstractions.SearchCriteria;

namespace ZhuoYue.Components.Code.NTest
{
    public class Tests
    {

        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZhuoYue_Components_Dev; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Order(-1)]
        public void TestCreation()
        {
            App app;
            using (var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new AppManager(new AppProvider(dbContext), new AppManagerOptions() { EnableCache = false });
                app = manager.Read(new Application.Abstractions.SearchCriteria()).First();
            }

            using (var dbContext = new CodeDbContext(new DbContextOptionsBuilder<CodeDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new CodeManager(new CodeProvider(dbContext), new CodeManagerOptions() { EnableCache = false });
                var codeCategory = new CodeCategory() { CreatedUserId = "EN", AppId = app.AppId, CategoryName = "Foo", Remarks = "Remarks", };
                codeCategory.CodeItems.Add(new CodeItem() { CodeName = "Foo1", CodeValue = "Bar1", Remarks = "Remarks1", Sequence = 10, CreatedUserId = "EN1" });
                codeCategory.CodeItems.Add(new CodeItem() { CodeName = "Foo2", CodeValue = "Bar2", Remarks = "Remarks2", Sequence = 10, CreatedUserId = "EN2" });
                codeCategory.CodeItems.Add(new CodeItem() { CodeName = "Foo3", CodeValue = "Bar3", Remarks = "Remarks3", Sequence = 10, CreatedUserId = "EN3" });
                var rt = manager.CreateCodeCategory(codeCategory);
                Assert.True(codeCategory == rt);
                Assert.True(!string.IsNullOrWhiteSpace(rt.AppId));
            }

            {
                var categories = new List<CodeCategory>();
                for (int i = 0; i < 50; i++)
                {
                    var category = new CodeCategory() { AppId = app.AppId, CategoryName = $"Category{i}", Remarks = $"CategoryRemarks{i}", CreatedUserId = $"EN{i}" };
                    for (int j = 0; j < 20; j++)
                    {
                        category.CodeItems.Add(new CodeItem() { Remarks = $"CodeRemarks{i}_{j}", CodeName = $"CodeName {i}_{j}", CodeValue = $"CodeValue {i}_{j}", Sequence = j, CreatedUserId = $"User{i}_{j}" });
                    }
                    categories.Add(category);

                }
                using (var dbContext = new CodeDbContext(new DbContextOptionsBuilder<CodeDbContext>().UseSqlServer(ConnectionString).Options))
                {
                    var manager = new CodeManager(new CodeProvider(dbContext), new CodeManagerOptions() { EnableCache = false });
                    var rt = manager.CreateCodeCategory(categories);
                    Assert.True(categories == rt);
                    Assert.True(rt.Count() == 50);
                }
            }
        }

        [Test]
        public void TestRead()
        {
            string appId, categoryId, categoryName;
            using (var dbContext = new CodeDbContext(new DbContextOptionsBuilder<CodeDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new CodeManager(new CodeProvider(dbContext), new CodeManagerOptions() { EnableCache = false });
                var rt = manager.ReadAll();
                Assert.True(rt.Count() > 0);
                var categories = rt.SelectMany(it => it.Select(_ => _));
                appId = categories.First().AppId;
                categoryId = categories.First().CategoryId;
                categoryName = categories.First().CategoryName;
            }
            using (var dbContext = new CodeDbContext(new DbContextOptionsBuilder<CodeDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new CodeManager(new CodeProvider(dbContext), new CodeManagerOptions() { EnableCache = false });
                var rt = manager.ReadAll(appId);
                Assert.True(!string.IsNullOrWhiteSpace(rt.AppId));
                Assert.True(rt.Count() > 0);
            }
            using (var dbContext = new CodeDbContext(new DbContextOptionsBuilder<CodeDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new CodeManager(new CodeProvider(dbContext), new CodeManagerOptions() { EnableCache = false });
                var searchCriteria = new SearchCriteria();
                searchCriteria.AppIds.Add(appId);
                searchCriteria.CategoryIds.Add(categoryId);
                searchCriteria.CategoryNames.Add(categoryName);
                searchCriteria.OrderBy.Add((nameof(CodeCategory.CategoryName), Core.AscOrDesc.Desc));
                var rt = manager.ReadCodeCategory(searchCriteria);
                Assert.True(!string.IsNullOrWhiteSpace(rt.First().AppId));
                Assert.True(rt.Count() > 0);
            }
        }

        [Test]
        public void TestDeletion()
        {
            IEnumerable<CodeCategory> categories;
            using (var dbContext = new CodeDbContext(new DbContextOptionsBuilder<CodeDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new CodeManager(new CodeProvider(dbContext), new CodeManagerOptions() { EnableCache = false });
                var rt = manager.ReadAll();
                Assert.True(rt.Count() > 0);
                categories = rt.SelectMany(it => it.Select(_ => _)).Take(2);
            }
            using (var dbContext = new CodeDbContext(new DbContextOptionsBuilder<CodeDbContext>().UseSqlServer(ConnectionString).Options))
            {
                var manager = new CodeManager(new CodeProvider(dbContext), new CodeManagerOptions() { EnableCache = false });
                manager.DeleteCodeCategory(categories);
            }
        }
    }
}