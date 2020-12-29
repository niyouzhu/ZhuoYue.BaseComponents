using System;
using System.ComponentModel.DataAnnotations.Schema;
using ZhuoYue.Components.Application.Abstractions;

namespace ZhuoYue.Components.Application.EntityFrameworkCore
{
    [Table("Application")]
    public class AppEntity : App
    {
    }
}
