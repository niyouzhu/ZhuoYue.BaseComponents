using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ZhuoYue.Components.Application.Abstractions;

namespace ZhuoYue.Components.Code.Abstractions
{
    public class AppCode : Collection<CodeCategory>
    {

        [Key]
        public string AppId
        {
            get
            {
                if (Count > 0) return this[0].AppId;
                return null;
            }
        }

    }
}