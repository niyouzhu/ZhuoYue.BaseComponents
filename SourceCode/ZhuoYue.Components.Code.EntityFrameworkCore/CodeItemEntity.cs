using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZhuoYue.Components.Code.Abstractions;

namespace ZhuoYue.Components.Code.EntityFrameworkCore
{
    [Table(name: "Codes")]
    public class CodeItemEntity : CodeItem
    {
    }
}
