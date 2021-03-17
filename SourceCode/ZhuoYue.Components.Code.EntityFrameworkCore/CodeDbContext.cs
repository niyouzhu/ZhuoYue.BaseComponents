using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZhuoYue.Components.Code.Abstractions;

namespace ZhuoYue.Components.Code.EntityFrameworkCore
{
    public class CodeDbContext : DbContext
    {
        public DbSet<CodeCategory> CodeCategories { get; set; }
        public DbSet<CodeItem> CodeItems { get; set; }

        public CodeDbContext(DbContextOptions<CodeDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeItem>().HasIndex(it => it.CodeCategoryId);
        }
    }
}
