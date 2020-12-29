using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZhuoYue.Components.Code.Abstractions
{
    [Table("Application")]
    public class ApplicationEntity
    {
        [Key]
        public string ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public string Remarks { get; set; }


    }
}