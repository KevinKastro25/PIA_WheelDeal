using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PIA_WheelDeal.Models
{
    public class PeticionCompraHR
    {
        public int IdPeticion { get; set; }
        public int IdInd { get; set; }
        public int IdProd { get; set; }
        public int IdStatus { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdEmpleado { get; set; }

    }
}
