using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PIA_WheelDeal.Models
{
	public class VentaHR
	{
		public int IdIngreso { get; set; }
		public int IdInd { get; set; }
		public int IdProd { get; set; }
		public int Monto { get; set; }
		public string? Descripcion { get; set; }
		public DateTime Fecha { get; set; }
	}
}
