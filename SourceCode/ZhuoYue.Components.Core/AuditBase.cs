using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZhuoYue.Components.Core
{
    public abstract class AuditBase
    {
        [MaxLength(256)]
        public string CreatedUserId { get; set; }

        public DateTime CreatedTime { get; set; }

        [MaxLength(256)]
        public string LastUpdatedUserId { get; set; }

        public DateTime? LastUpdatedTime { get; set; }
    }
}
