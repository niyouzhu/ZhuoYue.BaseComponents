using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using ZhuoYue.Components.Application.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace ZhuoYue.Components.Empty
{
    class Program
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZhuoYue_Components_Dev; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults.ConfigureServices((host, services) =>
            {
                services.AddAppDbContext(options => { options.UseSqlServer(ConnectionString); });
            }).Build().Run();

        }
    }
}
