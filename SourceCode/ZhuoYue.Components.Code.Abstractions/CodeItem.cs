using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZhuoYue.Components.Core;

namespace ZhuoYue.Components.Code.Abstractions
{

    public class CodeItem : AuditBase
    {

        [Key]
        [MaxLength(36)]

        public string CodeId { get; set; }
        [MaxLength(256)]


        public string CodeName { get; set; }
        [MaxLength(4096)]

        public string Remarks { get; set; }
        [MaxLength(256)]

        public string CodeValue { get; set; }
        public virtual int Sequence { get; set; }

        [MaxLength(36)]
        public virtual string CodeCategoryId { get; set; }
        [ForeignKey("CodeCategoryId")]
        public virtual CodeCategory CodeCategory { get; set; }
    }

}