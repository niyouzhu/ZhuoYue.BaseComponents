using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using ZhuoYue.Components.Application;
using ZhuoYue.Components.Application.Abstractions;
using ZhuoYue.Components.Application.EntityFrameworkCore;
using ZhuoYue.Components.Code.Abstractions;
using ZhuoYue.Components.Code.EntityFrameworkCore;

namespace ZhuoYue.Components.Code.Test
{
    public class UnitTest1
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZhuoYue_Components_Dev; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [Fact]
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
                codeCategory.CodeItems.Add(new CodeItem() { CodeName = "Foo", CodeValue = "Bar", Remarks = "Remarks", Sequence = 10, CreatedUserId = "EN" });
                var rt = manager.CreateCodeCategory(codeCategory);
                Assert.True(codeCategory != rt);
                Assert.True(!string.IsNullOrWhiteSpace(rt.AppId));
            }
        }
    }
}
