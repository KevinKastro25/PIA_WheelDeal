using System.ComponentModel.DataAnnotations.Schema;

namespace PIA_WheelDeal.Models.DTO
{
    public class SolicitudDTO
    {
        public int IdPeticion { get; set; }

        public int IdInd { get; set; }

        public int IdProd { get; set; }

        public int IdStatus { get; set; }

        public DateTime Fecha { get; set; }
    }
}
