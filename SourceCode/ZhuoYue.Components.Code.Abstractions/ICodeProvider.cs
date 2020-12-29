using System;
using System.Collections.Generic;
using System.Text;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Code.Abstractions
{
    public interface ICodeProvider : IProvider
    {
        CodeCategory CreateCodeCategory(CodeCategory codeCategory);

        IEnumerable<CodeCategory> CreateCodeCategory(IEnumerable<CodeCategory> codeCategories);

        IEnumerable<CodeCategory> ReadCodeCategory(CodeCategorySearchCriteria searchCriteria);

        CodeCategory UpdateCodeCategory(CodeCategory codeCategory);

        IEnumerable<CodeCategory> UpdateCodeCategory(IEnumerable<CodeCategory> codeCategories);
        CodeCategory DeleteCodeCategory(CodeCategory codeCategory);

        IEnumerable<CodeCategory> DeleteCodeCategory(IEnumerable<CodeCategory> codeCategories);

        CodeItem CreateCodeItem(CodeItem codeItem);
        IEnumerable<CodeItem> CreateCodeItem(IEnumerable<CodeItem> codeItems);

        IEnumerable<CodeItem> ReadCodeItem(CodeItemSearchCriteria codeItemSearchCriteria);
        CodeItem UpdateCodeItem(CodeItem codeItem);
        IEnumerable<CodeItem> UpdateCodeItem(IEnumerable<CodeItem> codeItems);
        CodeItem DeleteCodeItem(CodeItem codeItem);
        IEnumerable<CodeItem> DeleteCodeItem(IEnumerable<CodeItem> codeItems);


    }
}
