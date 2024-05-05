using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PIA_WheelDeal.Models.dbModels
{
    public class RolesCatalogo
    {
        [Key]
        [Column("id_rol")]
        public int IdRol { get; set; }

        [Column("rol")]
        [StringLength(50)]
        public string Rol { get; set; } = null!;

        [InverseProperty("IdRolNavigation")]
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; } = new List<ApplicationUser>();
    }
}
