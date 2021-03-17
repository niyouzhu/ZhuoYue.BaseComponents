using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZhuoYue.Components.Code.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddCodeDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContextPool<CodeDbContext>(options);
            return services;

        }
    }
}
