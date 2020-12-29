using System;
using System.Collections.Generic;
using System.Text;

namespace ZhuoYue.Components.Application.Abstractions
{
    public interface IAppManager
    {
        IAppProvider Provider { get; }
        IEnumerable<App> ReadAll();
        App Create(App app);

        IEnumerable<App> Create(IEnumerable<App> apps);

        IEnumerable<App> Read(SearchCriteria searchCriteria);

        App ReadOne(SearchCriteria searchCriteria);

        App Update(App app);


        IEnumerable<App> Update(IEnumerable<App> apps);

        App Delete(App app);

        IEnumerable<App> Delete(IEnumerable<App> apps);
    }
}
