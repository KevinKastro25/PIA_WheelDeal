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
    
    public class PeticionCompraController : Controller
    {
        
        private readonly BaseDeGatosContext _context;

        public PeticionCompraController(BaseDeGatosContext context)
        {
            _context = context;
        }

        // GET: PeticionCompra
        public async Task<IActionResult> Index()
        {
            var baseDeGatosContext = _context.PeticionCompras.Include(p => p.IdEmpleadoNavigation).Include(p => p.IdIndNavigation).Include(p => p.IdProdNavigation).Include(p => p.IdStatusNavigation);
            return View(await baseDeGatosContext.ToListAsync());
        }
        [Authorize]
        // GET: PeticionCompra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peticionCompra = await _context.PeticionCompras
                .Include(p => p.IdEmpleadoNavigation)
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

        // GET: PeticionCompra/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleado"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd");
            ViewData["IdStatus"] = new SelectList(_context.StatusCatalogos, "IdStatus", "IdStatus");
            return View();
        }

        
        // POST: PeticionCompra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPeticion,IdInd,IdProd,IdStatus,Fecha,IdEmpleado")] PeticionCompraHR peticionCompra)
        {
            if (ModelState.IsValid)
            {
                PeticionCompra peticionCompra2 = new PeticionCompra
                {
                    IdPeticion = peticionCompra.IdPeticion,
                    IdInd = peticionCompra.IdInd,
                    IdProd = peticionCompra.IdProd,
                    IdStatus = peticionCompra.IdStatus,
                    Fecha = peticionCompra.Fecha,
                    IdEmpleado = peticionCompra.IdEmpleado

                };
                _context.Add(peticionCompra2);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdEmpleado);
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", peticionCompra.IdProd);
            ViewData["IdStatus"] = new SelectList(_context.StatusCatalogos, "IdStatus", "IdStatus", peticionCompra.IdStatus);
            return View(peticionCompra);
        }

        // GET: PeticionCompra/Edit/5
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
            ViewData["IdEmpleado"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdEmpleado);
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", peticionCompra.IdProd);
            ViewData["IdStatus"] = new SelectList(_context.StatusCatalogos, "IdStatus", "IdStatus", peticionCompra.IdStatus);
            return View(peticionCompra);
        }

        // POST: PeticionCompra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPeticion,IdInd,IdProd,IdStatus,Fecha,IdEmpleado")] PeticionCompra peticionCompra)
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
            ViewData["IdEmpleado"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdEmpleado);
            ViewData["IdInd"] = new SelectList(_context.Users, "Id", "Id", peticionCompra.IdInd);
            ViewData["IdProd"] = new SelectList(_context.Vehiculos, "IdProd", "IdProd", peticionCompra.IdProd);
            ViewData["IdStatus"] = new SelectList(_context.StatusCatalogos, "IdStatus", "IdStatus", peticionCompra.IdStatus);
            return View(peticionCompra);
        }

        // GET: PeticionCompra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peticionCompra = await _context.PeticionCompras
                .Include(p => p.IdEmpleadoNavigation)
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

        // POST: PeticionCompra/Delete/5
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
