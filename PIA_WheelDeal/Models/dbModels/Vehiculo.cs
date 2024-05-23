using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_WheelDeal.Models.dbModels;

public partial class Vehiculo
{
    [Key]
    [Column("id_prod")]
    public int IdProd { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("id_tipo")]
    public int IdTipo { get; set; }

    [Column("precio")]
    public int Precio { get; set; }

    [Column("matricula")]
    [StringLength(50)]
    [Unicode(false)]
    public string Matricula { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(500)]
    public string? Descripcion { get; set; }

    [Column("disponible")]
    public bool? Disponible { get; set; }

    [Column("img")]
    [StringLength(500)]
    public string? Img { get; set; }

    [ForeignKey("IdTipo")]
    [InverseProperty("Vehiculos")]
    public virtual TiposCatalogo IdTipoNavigation { get; set; } = null!;

    [InverseProperty("IdProdNavigation")]
    public virtual ICollection<PeticionCompra> PeticionCompras { get; set; } = new List<PeticionCompra>();

    [InverseProperty("IdProdNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
