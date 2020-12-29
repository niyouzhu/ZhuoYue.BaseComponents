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
        public DbSet<CodeCategoryEntity> CodeCategories { get; set; }
        public DbSet<CodeItemEntity> CodeItems { get; set; }

    }
}
