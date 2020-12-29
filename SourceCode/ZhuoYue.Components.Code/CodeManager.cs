using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public CodeCategory CreateCodeCategory(CodeCategory codeCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeCategory> CreateCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeCategory> ReadCodeCategory(CodeCategorySearchCriteria searchCriteria)
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

        public CodeCategory DeleteCodeCategory(CodeCategory codeCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeCategory> DeleteCodeCategory(IEnumerable<CodeCategory> codeCategories)
        {
            throw new NotImplementedException();
        }

        public CodeItem CreateCodeItem(CodeItem codeItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeItem> CreateCodeItem(IEnumerable<CodeItem> codeItems)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeItem> ReadCodeItem(CodeItemSearchCriteria codeItemSearchCriteria)
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
    }
}
