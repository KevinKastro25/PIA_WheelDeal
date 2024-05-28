using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_WheelDeal.Models.dbModels;
using PIA_WheelDeal.Models.DTO;

namespace PIA_WheelDeal.Controllers
{
    //Controlador Exclusivo de Admins
    [Authorize(Roles = "Admin")]
    public class VehiculoesController : Controller
    {
        private readonly BaseDeGatosContext _context;

        public VehiculoesController(BaseDeGatosContext context)
        {
            _context = context;
        }

        // GET: Vehiculoes
        public async Task<IActionResult> Index()
        {
            var baseDeGatosContext = _context.Vehiculos.Include(v => v.IdTipoNavigation);
            return View(await baseDeGatosContext.ToListAsync());
        }

        // GET: Vehiculoes/Details/5
        public async Task<IActionResult> Details(int? id)
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

		// GET: Vehiculoes/Create
		public IActionResult Create()
		{
			VehiculoDTO vehiculotipo = new VehiculoDTO
			{
				TiposCatalogo = new SelectList(_context.TiposCatalogos, "IdTipo", "Tipo")
			};
			return View(vehiculotipo);
		}

		// POST: Vehiculoes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehiculoDTO vehiculo)
        {
            if (ModelState.IsValid)
            {
				Vehiculo vehiculo1 = new Vehiculo
				{
					Nombre = vehiculo.Nombre,
					IdTipo = vehiculo.IdTipo,
					Precio = vehiculo.Precio,
					Matricula = vehiculo.Matricula,
					Descripcion = vehiculo.Descripcion,
					Disponible = vehiculo.Disponible,
                    Img = vehiculo.Img
				};
				_context.Add(vehiculo1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
			vehiculo.TiposCatalogo = new SelectList(_context.TiposCatalogos, "IdTipo", "Tipo", vehiculo.IdTipo);
			return View(vehiculo);
		}

        // GET: Vehiculoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            ViewData["IdTipo"] = new SelectList(_context.TiposCatalogos, "IdTipo", "IdTipo", vehiculo.IdTipo);
            return View(vehiculo);
        }

        // POST: Vehiculoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProd,Nombre,IdTipo,Precio,Matricula,Descripcion,Disponible,Img")] Vehiculo vehiculo)
        {
            if (id != vehiculo.IdProd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
				Vehiculo vehiculo2 = new Vehiculo
				{
					IdProd = vehiculo.IdProd,
					Nombre = vehiculo.Nombre,
					IdTipo = vehiculo.IdTipo,
					Precio = vehiculo.Precio,
					Matricula = vehiculo.Matricula,
					Descripcion = vehiculo.Descripcion,
					Disponible = vehiculo.Disponible,
                    Img = vehiculo.Img
                };

				try
                {
                    _context.Update(vehiculo2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.IdProd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipo"] = new SelectList(_context.TiposCatalogos, "IdTipo", "IdTipo", vehiculo.IdTipo);
            return View(vehiculo);
        }

        // GET: Vehiculoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Vehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.IdProd == id);
        }
    }
}
