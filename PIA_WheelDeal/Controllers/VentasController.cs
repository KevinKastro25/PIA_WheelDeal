using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_WheelDeal.Models;
using PIA_WheelDeal.Models.dbModels;

namespace PIA_WheelDeal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VentasController : Controller
    {
        private readonly BaseDeGatosContext _context;

        public VentasController(BaseDeGatosContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var baseDeGatosContext = _context.Ventas.Include(v => v.IdIndNavigation).Include(v => v.IdProdNavigation);
            return View(await baseDeGatosContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdIndNavigation)
                .Include(v => v.IdProdNavigation)
                .FirstOrDefaultAsync(m => m.IdIngreso == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIngreso,IdInd,IdProd,Monto,Descripcion,Fecha")] VentaHR venta)
        {
            if (ModelState.IsValid)
            {
				Venta venta1 = new Venta
				{
					IdIngreso = venta.IdIngreso,
					IdInd = venta.IdInd,
					IdProd = venta.IdProd,
					Monto = venta.Monto,
					Descripcion = venta.Descripcion,
					Fecha = venta.Fecha
				};
				_context.Add(venta1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", venta.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", venta.IdProd);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", venta.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", venta.IdProd);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIngreso,IdInd,IdProd,Monto,Descripcion,Fecha")] Venta venta)
        {
            if (id != venta.IdIngreso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdIngreso))
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
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", venta.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", venta.IdProd);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdIndNavigation)
                .Include(v => v.IdProdNavigation)
                .FirstOrDefaultAsync(m => m.IdIngreso == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.IdIngreso == id);
        }
    }
}
