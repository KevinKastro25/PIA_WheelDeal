using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_WheelDeal.Models.dbModels;
using PIA_WheelDeal.Models.DTO;
using PIA_WheelDeal.Models.ViewModels;

namespace PIA_WheelDeal.Controllers
{
    public class PeticionesController : Controller
    {
        private readonly BaseDeGatosContext _context;
        public PeticionesController(BaseDeGatosContext context)
        {
            _context = context;
        }

        //GETEAR LOS PARAMETROS DEL VEHICULO SELECCIONADO
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var vehiculo = await _context.Vehiculos
                .Include(v => v.IdTipoNavigation)
                .FirstOrDefaultAsync(m => m.IdProd == id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            PeticionViewmodel peticion2 = new PeticionViewmodel()
            {
                Peticion = new PeticionCompraDTO()
                {
                    IdProd = vehiculo.IdProd
                }
            };
            return View(peticion2);
        }

        // POST: PeticionCompras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeticionCompraDTO peticion)
        {
            if (ModelState.IsValid)
            {
                if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier),out int currentuserid))
                {
					PeticionCompra peticion1 = new PeticionCompra()
                    {
                        IdInd = currentuserid,
                        IdProd = peticion.IdProd,
                        IdStatus = 1,
                        Fecha = DateTime.Now

					};
                    
                    _context.Add(peticion1);
					await _context.SaveChangesAsync();
					return RedirectToAction("Index", "Home");
				}
                
            }
            return BadRequest();
        }
    }
}
