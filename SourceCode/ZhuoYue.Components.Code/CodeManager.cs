using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuoYue.Components.Code.Abstractions;

namespace ZhuoYue.Components.Code
{
    public class CodeManager : ICodeManager
    {
        public ICodeProvider CodeProvider { get; }

        public CodeManagerOptions Options { get; }
        public CodeManager(ICodeProvider codeProvider, CodeManagerOptions options)
        {
            CodeProvider = codeProvider;
            Options = options;
        }

        public AppCode ReadAll(string appId)
        {
            var searchCriteria = new SearchCriteria();
            searchCriteria.AppIds.Add(appId);
            searchCriteria.PageSize = null;
            var result = ReadCodeCategory(searchCriteria);
            var rt = new AppCode();
            rt.AddRange(result);
            return rt;
        }

        public CodeCategory CreateCodeCategory(CodeCategory codeCategory)
        {
            return CodeProvider.CreateCodeCategory(codeCategory);
        }

        public IEnumerable<CodeCategory> CreateCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            return CodeProvider.CreateCodeCategory(codeCategories);
        }

        public IEnumerable<CodeCategory> ReadCodeCategory(SearchCriteria searchCriteria)
        {
            return CodeProvider.ReadCodeCategory(searchCriteria);
        }

        public CodeCategory UpdateCodeCategory(CodeCategory codeCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeCategory> UpdateCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            throw new NotImplementedException();
        }

        public CodeCategory DeleteCodeCategory(CodeCategory codeCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeCategory> DeleteCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            return CodeProvider.DeleteCodeCategory(codeCategories);
        }

        public CodeItem CreateCodeItem(CodeItem codeItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeItem> CreateCodeItem(IEnumerable<CodeItem> codeItems)
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

        public CodeItem DeleteCodeItem(CodeItem codeItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeItem> DeleteCodeItem(IEnumerable<CodeItem> codeItems)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppCode> ReadAll()
        {
            var result = ReadCodeCategory(new SearchCriteria() { PageSize = null });
            var appIds = result.Select(it => it.AppId).Distinct();
            var rt = new List<AppCode>(appIds.Count());
            appIds.ForEach(it =>
            {
                var categories = result.Where(_ => _.AppId == it);
                var appCode = new AppCode();
                appCode.AddRange(categories);
                rt.Add(appCode);
            });
            return rt;
        }
    }
}
