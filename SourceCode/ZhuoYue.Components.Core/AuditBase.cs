using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZhuoYue.Components.Core
{
    public abstract class AuditBase
    {
        [MaxLength(256)]
        [Required]
        public string CreatedUserId { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        [MaxLength(256)]
        public string LastUpdatedUserId { get; set; }

        public DateTime? LastUpdatedTime { get; set; }
    }
}
