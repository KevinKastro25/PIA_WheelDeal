using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_WheelDeal.Models.dbModels;

public partial class Venta
{
    [Key]
    [Column("id_ingreso")]
    public int IdIngreso { get; set; }

    [Column("id_ind")]
    public int IdInd { get; set; }

    [Column("id_prod")]
    public int IdProd { get; set; }

    [Column("monto")]
    public int Monto { get; set; }

    [Column("descripcion")]
    [StringLength(500)]
    public string? Descripcion { get; set; }

    [Column("fecha", TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [ForeignKey("IdInd")]
    [InverseProperty("Venta")]
    public virtual ApplicationUser IdIndNavigation { get; set; } = null!;

    [ForeignKey("IdProd")]
    [InverseProperty("Venta")]
    public virtual Vehiculo IdProdNavigation { get; set; } = null!;
}
