using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PIA_WheelDeal.Models.dbModels
{
    public class ApplicationUser : IdentityUser<int>
    {
       
        public int IdRol { get; set; }

        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<PeticionCompra> PeticionCompraIdEmpleadoNavigations { get; set; } = new List<PeticionCompra>();

        [InverseProperty("IdIndNavigation")]
        public virtual ICollection<PeticionCompra> PeticionCompraIdIndNavigations { get; set; } = new List<PeticionCompra>();

        [InverseProperty("IdIndNavigation")]
        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
    }
}
