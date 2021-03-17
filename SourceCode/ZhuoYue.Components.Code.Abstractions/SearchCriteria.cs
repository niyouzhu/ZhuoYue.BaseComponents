using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Code.Abstractions
{
    public class SearchCriteria : SearchCriteriaBase
    {
        public ICollection<string> CategoryIds { get; } = new Collection<string>();
        public ICollection<string> AppIds { get; } = new Collection<string>();

        public ICollection<string> CategoryNames { get; } = new Collection<string>();

        public static SearchCriteria Default { get; } = new SearchCriteria();
    }
}