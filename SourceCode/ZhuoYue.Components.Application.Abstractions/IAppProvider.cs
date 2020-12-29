using System;
using System.Collections.Generic;
using System.Text;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Application.Abstractions
{
    public interface IAppProvider : IProvider
    {

        App Create(App app);

        IEnumerable<App> Create(IEnumerable<App> apps);

        IEnumerable<App> Read(SearchCriteria searchCriteria);

        App Update(App app);


        IEnumerable<App> Update(IEnumerable<App> apps);

        App Delete(App app);

        IEnumerable<App> Delete(IEnumerable<App> apps);
    }
}
