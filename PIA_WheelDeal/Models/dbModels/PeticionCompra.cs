using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_WheelDeal.Models.dbModels;

[Table("PeticionCompra")]
public partial class PeticionCompra
{
    [Key]
    [Column("id_peticion")]
    public int IdPeticion { get; set; }

    [Column("id_ind")]
    public int IdInd { get; set; }

    [Column("id_prod")]
    public int IdProd { get; set; }

    [Column("id_status")]
    public int IdStatus { get; set; }

    [Column("fecha", TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [ForeignKey("IdInd")]
    [InverseProperty("PeticionCompras")]
    public virtual ApplicationUser IdIndNavigation { get; set; } = null!;

    [ForeignKey("IdProd")]
    [InverseProperty("PeticionCompras")]
    public virtual Vehiculo IdProdNavigation { get; set; } = null!;

    [ForeignKey("IdStatus")]
    [InverseProperty("PeticionCompras")]
    public virtual StatusCatalogo IdStatusNavigation { get; set; } = null!;
}
