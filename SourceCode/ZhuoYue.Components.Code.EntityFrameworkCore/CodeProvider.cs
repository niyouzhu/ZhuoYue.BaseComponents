using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ZhuoYue.Components.Code.Abstractions;

namespace ZhuoYue.Components.Code.EntityFrameworkCore
{
    public class CodeProvider : ICodeProvider
    {
        public CodeProvider(CodeDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public CodeDbContext DbContext { get; }

        public string ProviderName { get; set; } = "CodeProvider.EntityFrameworkCore";

        public CodeCategory CreateCodeCategory(CodeCategory codeCategory)
        {
            codeCategory.CategoryId = Guid.NewGuid().ToString();
            codeCategory.CodeItems.ForEach(it =>
            {
                it.CodeCategoryId = codeCategory.CategoryId;
                it.CodeId = Guid.NewGuid().ToString();
            });
            using (DbContext.Database.BeginTransaction())
            {
                var entity = DbContext.CodeCategories.Add(codeCategory);
                DbContext.CodeItems.AddRange(codeCategory.CodeItems);
                DbContext.SaveChanges();
                DbContext.Database.CommitTransaction();
                return entity.Entity;
            }
        }

        public IEnumerable<CodeCategory> CreateCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            codeCategories.ForEach(codeCategory =>
            {
                codeCategory.CategoryId = Guid.NewGuid().ToString();
                codeCategory.CodeItems.ForEach(it =>
                {
                    it.CodeCategoryId = codeCategory.CategoryId;
                    it.CodeId = Guid.NewGuid().ToString();
                });
            });
            using (DbContext.Database.BeginTransaction())
            {
                DbContext.CodeCategories.AddRange(codeCategories);
                codeCategories.ForEach(it =>
                {
                    DbContext.CodeItems.AddRange(it.CodeItems);
                });
                DbContext.SaveChanges();
                DbContext.Database.CommitTransaction();
                return codeCategories;
            }
        }

        public CodeItem CreateCodeItem(CodeItem codeItem)
        {
            codeItem.CodeId = Guid.NewGuid().ToString();
            var entity = DbContext.CodeItems.Add(codeItem);
            DbContext.SaveChanges();
            return entity.Entity;
        }

        public IEnumerable<CodeItem> CreateCodeItem(IEnumerable<CodeItem> codeItems)
        {
            codeItems.ForEach(it =>
            {
                it.CodeId = Guid.NewGuid().ToString();

            });
            DbContext.CodeItems.AddRange(codeItems);
            DbContext.SaveChanges();
            return codeItems;
        }

        public CodeCategory DeleteCodeCategory(CodeCategory codeCategory)
        {
            var codeItems = DbContext.CodeItems.Where(it => it.CodeCategoryId == codeCategory.CategoryId);
            var category = DbContext.CodeCategories.Find(codeCategory.CategoryId);
            using (DbContext.Database.BeginTransaction())
            {
                DbContext.CodeItems.RemoveRange(codeItems);
                DbContext.CodeCategories.Remove(category);
                DbContext.SaveChanges();
                DbContext.Database.CommitTransaction();
            }
            category.CodeItems.AddRange(codeItems);
            return category;

        }

        public IEnumerable<CodeCategory> DeleteCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            var searchCriteria = new SearchCriteria() { PageSize = null };
            searchCriteria.CategoryIds.AddRange(codeCategories.Select(it => it.CategoryId).Distinct());
            var rt = ReadCodeCategory(searchCriteria);
            using (DbContext.Database.BeginTransaction())
            {
                DbContext.CodeItems.RemoveRange(rt.SelectMany(it => it.CodeItems));
                DbContext.CodeCategories.RemoveRange(rt);
                DbContext.SaveChanges();
                DbContext.Database.CommitTransaction();
            }
            return rt;
        }

        public CodeItem DeleteCodeItem(CodeItem codeItem)
        {
            var entity = DbContext.CodeItems.Find(codeItem.CodeId);
            DbContext.CodeItems.Remove(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<CodeItem> DeleteCodeItem(IEnumerable<CodeItem> codeItems)
        {
            var codeIds = codeItems.Select(it => it.CodeId);
            codeItems = DbContext.CodeItems.Where(it => codeIds.Contains(it.CodeId));
            DbContext.CodeItems.RemoveRange(codeItems);
            DbContext.SaveChanges();
            return codeItems;
        }

        public IEnumerable<CodeCategory> ReadCodeCategory(SearchCriteria searchCriteria)
        {
            IQueryable<CodeCategory> queryable = DbContext.CodeCategories;
            if (searchCriteria.AppIds.Any())
            {
                queryable = queryable.Where(it => searchCriteria.AppIds.Contains(it.AppId));
            }
            if (searchCriteria.CategoryIds.Any())
            {
                queryable = queryable.Where(it => searchCriteria.CategoryIds.Contains(it.CategoryId));
            }
            if (searchCriteria.CategoryNames.Any())
            {
                queryable = queryable.Where(it => searchCriteria.CategoryNames.Contains(it.CategoryName));
            }
            if (searchCriteria.OrderBy.Any())
                queryable = queryable.OrderBy(searchCriteria.OrderBy).AsQueryable();

            //GroupJoin cannot work here, instead of Join
            var a = queryable.Join(DbContext.CodeItems, it => it.CategoryId, it => it.CodeCategoryId, (l, r) => new { Category = l, Item = r });
            if (searchCriteria.PageSize.HasValue && searchCriteria.PageSize != int.MaxValue)
            {
                if (searchCriteria.PageIndex > 0)
                {
                    a = a.Skip(searchCriteria.PageSize.Value * searchCriteria.PageIndex).Take(searchCriteria.PageSize.Value);
                }
                else
                {
                    a = a.Take(searchCriteria.PageSize.Value);
                }
            }
            return a.ToList().ToLookup(it => it.Category).Select(it => it.Key); ;
        }


        public CodeCategory UpdateCodeCategory(CodeCategory codeCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeCategory> UpdateCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            throw new NotImplementedException();
        }

        public CodeItem UpdateCodeItem(CodeItem codeItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeItem> UpdateCodeItem(IEnumerable<CodeItem> codeItems)
        {
            throw new NotImplementedException();
        }
    }
}
