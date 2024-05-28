using PIA_WheelDeal.Models.dbModels;
using PIA_WheelDeal.Models.DTO;

namespace PIA_WheelDeal.Models.ViewModels
{
	public class PeticionViewmodel
	{
        public PeticionCompraDTO? Peticion { get; set; }
        public Vehiculo? Producto { get; set; }
    }
}
