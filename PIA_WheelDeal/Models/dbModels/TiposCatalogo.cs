using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_WheelDeal.Models.dbModels;

[Table("TiposCatalogo")]
public partial class TiposCatalogo
{
    [Key]
    [Column("id_tipo")]
    public int IdTipo { get; set; }

    [Column("tipo")]
    [StringLength(50)]
    public string Tipo { get; set; } = null!;

    [InverseProperty("IdTipoNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
