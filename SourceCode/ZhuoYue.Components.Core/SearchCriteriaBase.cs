using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ZhuoYue.Components.Core
{
    public class SearchCriteriaBase
    {
        public int? PageSize { get; set; }
        public int PageIndex { get; set; }
        public Collection<ValueTuple<string, AscOrDesc?>> OrderBy { get; } = new Collection<(string, AscOrDesc?)>();
    }

    public enum AscOrDesc
    {
        Asc = 1, Desc = 2
    }
}
