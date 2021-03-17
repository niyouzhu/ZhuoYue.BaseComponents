using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;
using ZhuoYue.Components.Cache.Abstractions;
using ZhuoYue.Components.Code.Abstractions;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Code.Cache
{
    public class CodeProvider : ICodeProvider
    {

        private string GetKey(string key)
        {
            return $"{CachePrefix.Code}_{key}";

        }

        private void DeleteCache(string key)
        {
            key = GetKey(key);
            if (CacheManager.Exists(key)) CacheManager.Delete(key);
        }

        private void DeleteCache()
        {
            if (CacheManager.Exists(CachePrefix.Code)) CacheManager.Delete(CachePrefix.Code);
        }

        public ICodeProvider InternalProvider { get; }
        public ICacheManager CacheManager { get; }

        public string ProviderName { get; set; } = "CodeProvider.Cache";

        public CodeProvider(ICodeProvider internalProvider, ICacheManager cacheManager)
        {
            InternalProvider = internalProvider;
            CacheManager = cacheManager;
        }
        public CodeCategory CreateCodeCategory(CodeCategory codeCategory)
        {
            var rt = InternalProvider.CreateCodeCategory(codeCategory);
            DeleteCache(rt.AppId);
            DeleteCache();
            return rt;
        }

        public IEnumerable<CodeCategory> CreateCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            var rt = InternalProvider.CreateCodeCategory(codeCategories);
            rt.Select(it => it.AppId).Distinct().ForEach(it =>
            {
                DeleteCache(it);
            });
            DeleteCache();
            return rt;
        }

        public CodeItem CreateCodeItem(CodeItem codeItem)
        {
            var rt = InternalProvider.CreateCodeItem(codeItem);
            var searchCriteria = new SearchCriteria() { PageSize = 1 };
            searchCriteria.CategoryIds.Add(rt.CodeCategoryId);
            var appId = InternalProvider.ReadCodeCategory(searchCriteria).First().AppId;
            DeleteCache(appId);
            DeleteCache(rt.CodeCategoryId);
            DeleteCache();
            return rt;
        }

        public IEnumerable<CodeItem> CreateCodeItem(IEnumerable<CodeItem> codeItems)
        {
            var rt = InternalProvider.CreateCodeItem(codeItems);
            var categoryIds = codeItems.Select(it => it.CodeCategoryId).Distinct();
            var searchCriteria = new SearchCriteria() { PageSize = categoryIds.Count() };
            searchCriteria.CategoryIds.AddRange(categoryIds);
            var categories = InternalProvider.ReadCodeCategory(searchCriteria);
            categories.Select(it => it.AppId).Distinct().ForEach(it =>
            {
                DeleteCache(it);
            });
            categories.Select(it => it.CategoryId).Distinct().ForEach(it => { DeleteCache(it); });
            DeleteCache();
            return rt;
        }

        public CodeCategory DeleteCodeCategory(CodeCategory codeCategory)
        {
            var rt = InternalProvider.DeleteCodeCategory(codeCategory);
            DeleteCache(rt.CategoryId);
            DeleteCache(rt.AppId);
            DeleteCache();
            return rt;
        }

        public IEnumerable<CodeCategory> DeleteCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            var rt = InternalProvider.DeleteCodeCategory(codeCategories);
            rt.Select(it => it.AppId).Distinct().ForEach(it =>
            {
                DeleteCache(it);
            });

            rt.Select(it => it.CategoryId).Distinct().ForEach(it =>
              {
                  DeleteCache(it);
              });
            DeleteCache();
            return rt;
        }

        public CodeItem DeleteCodeItem(CodeItem codeItem)
        {
            var rt = InternalProvider.DeleteCodeItem(codeItem);
            var searchCriteria = new SearchCriteria() { PageSize = 1 };
            searchCriteria.CategoryIds.Add(rt.CodeCategoryId);
            var category = InternalProvider.ReadCodeCategory(searchCriteria).First();
            DeleteCache(category.AppId);
            DeleteCache(category.CategoryId);
            DeleteCache();
            return rt;
        }

        public IEnumerable<CodeItem> DeleteCodeItem(IEnumerable<CodeItem> codeItems)
        {
            var rt = InternalProvider.DeleteCodeItem(codeItems);
            var categoryIds = codeItems.Select(it => it.CodeCategoryId).Distinct();
            var searchCriteria = new SearchCriteria() { PageSize = categoryIds.Count() };
            searchCriteria.CategoryIds.AddRange(categoryIds);
            var categories = InternalProvider.ReadCodeCategory(searchCriteria);
            categories.Select(it => it.AppId).Distinct().ForEach(it =>
            {
                DeleteCache(it);
            });

            categories.Select(it => it.CategoryId).Distinct().ForEach(it =>
            {
                DeleteCache(it);
            });
            DeleteCache();
            return rt;
        }

        public IEnumerable<CodeCategory> ReadCodeCategory(SearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public CodeCategory UpdateCodeCategory(CodeCategory codeCategory)
        {
            var rt = InternalProvider.UpdateCodeCategory(codeCategory);
            if (CacheManager.Exists(CachePrefix.Code)) CacheManager.Delete(CachePrefix.Code);
            if (CacheManager.Exists($"{CachePrefix.Code}_{rt.AppId}")) CacheManager.Delete($"{CachePrefix.Code}_{rt.AppId}");
            return rt;
        }

        public IEnumerable<CodeCategory> UpdateCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            var rt = InternalProvider.UpdateCodeCategory(codeCategories);
            rt.Select(it => it.AppId).Distinct().ForEach(it =>
            {
                if (CacheManager.Exists($"{CachePrefix.Code}_{it}")) CacheManager.Delete($"{CachePrefix.Code}_{it}");

            });
            rt.Select(it => it.CategoryId).Distinct().ForEach(it =>
            {
                if (CacheManager.Exists($"{CachePrefix.Code}_{it}")) CacheManager.Delete($"{CachePrefix.Code}_{it}");

            });
            if (CacheManager.Exists(CachePrefix.Code)) CacheManager.Delete(CachePrefix.Code);

            return rt;
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
