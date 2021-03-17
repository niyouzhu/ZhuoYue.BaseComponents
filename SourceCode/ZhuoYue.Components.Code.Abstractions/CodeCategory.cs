using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZhuoYue.Components.Application.Abstractions;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Code.Abstractions
{

    public class CodeCategory : AuditBase
    {
        [Key]
        [MaxLength(36)]
        public string CategoryId { get; set; }
        [MaxLength(256)]
        public string CategoryName { get; set; }
        [MaxLength(4096)]
        public string Remarks { get; set; }
        [MaxLength(36)]
        public string AppId { get; set; }
        public virtual ICollection<CodeItem> CodeItems { get; } = new Collection<CodeItem>();
    }

}