using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace PIA_WheelDeal.Models
{
    public class VehiculoHR
    {
        public int IdProd { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdTipo { get; set; }
        public int Precio { get; set; }
        public string Matricula { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? Disponible { get; set; }
		public string? Img { get; set; }

		[JsonIgnore]
        [IgnoreDataMember]
        public SelectList? TiposCatalogo { get; set; }
    }
}
