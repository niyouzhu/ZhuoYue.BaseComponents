using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Application.Abstractions
{
    public class App : AuditBase
    {
        [Key]
        [MaxLength(36)]
        public virtual string AppId { get; set; }

        [MaxLength(256)]
        public virtual string AppName { get; set; }

        [MaxLength(4096)]
        public virtual string AppRemarks { get; set; }
    }
}
