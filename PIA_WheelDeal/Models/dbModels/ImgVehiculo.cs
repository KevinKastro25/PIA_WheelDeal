using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_WheelDeal.Models.dbModels;

public partial class ImgVehiculo
{
    [Key]
    [Column("id_img")]
    public int IdImg { get; set; }

    [Column("img")]
    [StringLength(100)]
    public string Img { get; set; } = null!;

    [Column("id_prod")]
    public int IdProd { get; set; }

    [ForeignKey("IdProd")]
    [InverseProperty("ImgVehiculos")]
    public virtual Vehiculo IdProdNavigation { get; set; } = null!;
}
