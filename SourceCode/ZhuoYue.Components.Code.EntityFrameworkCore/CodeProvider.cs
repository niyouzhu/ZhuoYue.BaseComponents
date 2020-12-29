using System;
using System.Collections.Generic;
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
            var rt = new CodeCategoryEntity()
            {
                AppId = codeCategory.AppId,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = codeCategory.CategoryName,
                CreatedTime = DateTime.Now,
                CreatedUserId = codeCategory.CreatedUserId,
                Remarks = codeCategory.Remarks
            };
            var items = new List<CodeItemEntity>(codeCategory.CodeItems.Count);

            codeCategory.CodeItems.ForEach(it =>
            {
                items.Add(new CodeItemEntity()
                {
                    CodeCategoryId = it.CodeCategoryId,
                    CodeId = Guid.NewGuid().ToString(),
                    CodeName = it.CodeName,
                    CodeValue = it.CodeValue,
                    CreatedTime = DateTime.Now,
                    CreatedUserId = it.CreatedUserId,
                    Remarks = it.Remarks,
                    Sequence = it.Sequence
                });
            });
            rt.CodeItems.AddRange(items);
            using (DbContext.Database.BeginTransaction())
            {
                var entity = DbContext.CodeCategories.Add(rt);
                DbContext.CodeItems.AddRange(items);
                DbContext.SaveChanges();
                rt.CategoryId = entity.Entity.CategoryId;
            }
            return rt;
        }

        public IEnumerable<CodeCategory> CreateCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            throw new NotImplementedException();
        }

        public CodeItem CreateCodeItem(CodeItem codeItem)
        {
            var rt = new CodeItemEntity()
            {
                CodeCategoryId = codeItem.CodeCategoryId,
                CodeId = Guid.NewGuid().ToString(),
                CodeName = codeItem.CodeName,
                CodeValue = codeItem.CodeValue,
                CreatedTime = DateTime.Now,
                CreatedUserId = codeItem.CreatedUserId,
                Remarks = codeItem.Remarks,
                Sequence = codeItem.Sequence
            };
            DbContext.CodeItems.Add(rt);
            DbContext.SaveChanges();
            return rt;
        }

        public IEnumerable<CodeItem> CreateCodeItem(IEnumerable<CodeItem> codeItems)
        {
            var rt = new List<CodeItemEntity>(codeItems.Count());
            codeItems.ForEach(it =>
            {
                rt.Add(new CodeItemEntity()
                {
                    CodeCategoryId = it.CodeCategoryId,
                    CodeId = Guid.NewGuid().ToString(),
                    CodeName = it.CodeName,
                    CodeValue = it.CodeValue,
                    CreatedTime = DateTime.Now,
                    CreatedUserId = it.CreatedUserId,
                    Remarks = it.Remarks,
                    Sequence = it.Sequence
                });

            });
            DbContext.CodeItems.AddRange(rt);
            DbContext.SaveChanges();
            return rt;
        }

        public CodeCategory DeleteCodeCategory(CodeCategory codeCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeCategory> DeleteCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            throw new NotImplementedException();
        }

        public CodeItem DeleteCodeItem(CodeItem codeItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeItem> DeleteCodeItem(IEnumerable<CodeItem> codeItems)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeCategory> ReadCodeCategory(CodeCategorySearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeItem> ReadCodeItem(CodeItemSearchCriteria codeItemSearchCriteria)
        {
            throw new NotImplementedException();
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
