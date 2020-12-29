using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZhuoYue.Components.Application.Abstractions;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Code.Abstractions
{
    [Table("CodeCategories")]
    public class CodeCategory : AuditBase
    {
        [Key]
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Remarks { get; set; }
        [ForeignKey("App")]
        public string AppId { get; set; }

        public virtual App App { get; set; }

        public ICollection<CodeItem> CodeItems { get; } = new Collection<CodeItem>();
    }
}