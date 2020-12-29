using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Code.Abstractions
{
    [Table("CodeItems")]
    public class CodeItem : AuditBase
    {
        [Key]
        public string CodeId { get; set; }

        public string CodeName { get; set; }
        public string Remarks { get; set; }
        public string CodeValue { get; set; }
        public virtual int Sequence { get; set; }

        [ForeignKey("CodeCategory")]
        public virtual string CodeCategoryId { get; set; }
        public virtual CodeCategory CodeCategory { get; set; }
    }
}