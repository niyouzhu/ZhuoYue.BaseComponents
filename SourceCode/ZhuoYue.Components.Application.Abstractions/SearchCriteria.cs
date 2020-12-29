using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Application.Abstractions
{
    public class SearchCriteria : SearchCriteriaBase
    {
        public ICollection<string> AppId { get; } = new Collection<string>();
        public ICollection<string> AppName { get; } = new Collection<string>();


    }
}