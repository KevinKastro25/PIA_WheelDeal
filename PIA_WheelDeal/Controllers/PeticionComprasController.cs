using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_WheelDeal.Models.dbModels;

namespace PIA_WheelDeal.Controllers
{
    public class PeticionComprasController : Controller
    {
        private readonly BaseDeGatosContext _context;

        public PeticionComprasController(BaseDeGatosContext context)
        {
            _context = context;
        }

        // GET: PeticionCompras
        public async Task<IActionResult> Index()
        {
            var baseDeGatosContext = _context.PeticionCompras.Include(p => p.IdIndNavigation).Include(p => p.IdProdNavigation).Include(p => p.IdStatusNavigation);
            return View(await baseDeGatosContext.ToListAsync());
        }

        // GET: PeticionCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peticionCompra = await _context.PeticionCompras
                .Include(p => p.IdIndNavigation)
                .Include(p => p.IdProdNavigation)
                .Include(p => p.IdStatusNavigation)
                .FirstOrDefaultAsync(m => m.IdPeticion == id);
            if (peticionCompra == null)
            {
                return NotFound();
            }

            return View(peticionCompra);
        }

        // GET: PeticionCompras/Create
        public IActionResult Create()
        {
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd");
            ViewData["IdStatus"] = new SelectList(_context.StatusCatalogos, "IdStatus", "IdStatus");
            return View();
        }

        // POST: PeticionCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPeticion,IdInd,IdProd,IdStatus,Fecha")] PeticionCompra peticionCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peticionCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", peticionCompra.IdProd);
            ViewData["IdStatus"] = new SelectList(_context.StatusCatalogos, "IdStatus", "IdStatus", peticionCompra.IdStatus);
            return View(peticionCompra);
        }

        // GET: PeticionCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peticionCompra = await _context.PeticionCompras.FindAsync(id);
            if (peticionCompra == null)
            {
                return NotFound();
            }
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", peticionCompra.IdProd);
            ViewData["IdStatus"] = new SelectList(_context.StatusCatalogos, "IdStatus", "IdStatus", peticionCompra.IdStatus);
            return View(peticionCompra);
        }

        // POST: PeticionCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPeticion,IdInd,IdProd,IdStatus,Fecha")] PeticionCompra peticionCompra)
        {
            if (id != peticionCompra.IdPeticion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peticionCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeticionCompraExists(peticionCompra.IdPeticion))
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
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", peticionCompra.IdProd);
            ViewData["IdStatus"] = new SelectList(_context.StatusCatalogos, "IdStatus", "IdStatus", peticionCompra.IdStatus);
            return View(peticionCompra);
        }

        // GET: PeticionCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peticionCompra = await _context.PeticionCompras
                .Include(p => p.IdIndNavigation)
                .Include(p => p.IdProdNavigation)
                .Include(p => p.IdStatusNavigation)
                .FirstOrDefaultAsync(m => m.IdPeticion == id);
            if (peticionCompra == null)
            {
                return NotFound();
            }

            return View(peticionCompra);
        }

        // POST: PeticionCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var peticionCompra = await _context.PeticionCompras.FindAsync(id);
            if (peticionCompra != null)
            {
                _context.PeticionCompras.Remove(peticionCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeticionCompraExists(int id)
        {
            return _context.PeticionCompras.Any(e => e.IdPeticion == id);
        }
    }
}
