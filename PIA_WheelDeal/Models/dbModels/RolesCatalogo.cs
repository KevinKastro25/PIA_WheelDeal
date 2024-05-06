using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_WheelDeal.Models.dbModels;

[Table("RolesCatalogo")]
public partial class RolesCatalogo
{
    [Key]
    [Column("id_rol")]
    public int IdRol { get; set; }

    [Column("rol")]
    [StringLength(50)]
    public string Rol { get; set; } = null!;
}
