using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZhuoYue.Components.Application.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContextPool<AppDbContext>(options);
            return services;
        }
    }
}
