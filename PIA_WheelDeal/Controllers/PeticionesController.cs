using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_WheelDeal.Models.dbModels;

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

            return View(vehiculo);
        }

        // POST: PeticionCompras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeticionCompra peticionCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peticionCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","HomeController");
            }
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", peticionCompra.IdProd);
            ViewData["IdStatus"] = new SelectList(_context.StatusCatalogos, "IdStatus", "IdStatus", peticionCompra.IdStatus);
            return View(peticionCompra);
        }
    }
}
