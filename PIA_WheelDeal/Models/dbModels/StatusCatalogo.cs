using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_WheelDeal.Models.dbModels;

[Table("StatusCatalogo")]
public partial class StatusCatalogo
{
    [Key]
    [Column("id_status")]
    public int IdStatus { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdStatusNavigation")]
    public virtual ICollection<PeticionCompra> PeticionCompras { get; set; } = new List<PeticionCompra>();
}
