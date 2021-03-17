using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZhuoYue.Components.Application.EntityFrameworkCore
{
    public class AppDbContext : DbContext
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AppEntity> Apps { get; set; }


    }
}
