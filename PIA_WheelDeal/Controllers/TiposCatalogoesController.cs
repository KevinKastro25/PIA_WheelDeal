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
    public class TiposCatalogoesController : Controller
    {
        private readonly BaseDeGatosContext _context;

        public TiposCatalogoesController(BaseDeGatosContext context)
        {
            _context = context;
        }

        // GET: TiposCatalogoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposCatalogos.ToListAsync());
        }

        // GET: TiposCatalogoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposCatalogo = await _context.TiposCatalogos
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tiposCatalogo == null)
            {
                return NotFound();
            }

            return View(tiposCatalogo);
        }

        // GET: TiposCatalogoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposCatalogoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipo,Tipo")] TiposCatalogo tiposCatalogo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposCatalogo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposCatalogo);
        }

        // GET: TiposCatalogoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposCatalogo = await _context.TiposCatalogos.FindAsync(id);
            if (tiposCatalogo == null)
            {
                return NotFound();
            }
            return View(tiposCatalogo);
        }

        // POST: TiposCatalogoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipo,Tipo")] TiposCatalogo tiposCatalogo)
        {
            if (id != tiposCatalogo.IdTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposCatalogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposCatalogoExists(tiposCatalogo.IdTipo))
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
            return View(tiposCatalogo);
        }

        // GET: TiposCatalogoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposCatalogo = await _context.TiposCatalogos
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tiposCatalogo == null)
            {
                return NotFound();
            }

            return View(tiposCatalogo);
        }

        // POST: TiposCatalogoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposCatalogo = await _context.TiposCatalogos.FindAsync(id);
            if (tiposCatalogo != null)
            {
                _context.TiposCatalogos.Remove(tiposCatalogo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposCatalogoExists(int id)
        {
            return _context.TiposCatalogos.Any(e => e.IdTipo == id);
        }
    }
}
