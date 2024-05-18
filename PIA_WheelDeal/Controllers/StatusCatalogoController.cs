using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_WheelDeal.Models.dbModels;

namespace PIA_WheelDeal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatusCatalogoController : Controller
    {
        private readonly BaseDeGatosContext _context;

        public StatusCatalogoController(BaseDeGatosContext context)
        {
            _context = context;
        }

        // GET: StatusCatalogo
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusCatalogos.ToListAsync());
        }

        // GET: StatusCatalogo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusCatalogo = await _context.StatusCatalogos
                .FirstOrDefaultAsync(m => m.IdStatus == id);
            if (statusCatalogo == null)
            {
                return NotFound();
            }

            return View(statusCatalogo);
        }

        // GET: StatusCatalogo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusCatalogo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdStatus,Descripcion")] StatusCatalogo statusCatalogo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusCatalogo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusCatalogo);
        }

        // GET: StatusCatalogo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusCatalogo = await _context.StatusCatalogos.FindAsync(id);
            if (statusCatalogo == null)
            {
                return NotFound();
            }
            return View(statusCatalogo);
        }

        // POST: StatusCatalogo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdStatus,Descripcion")] StatusCatalogo statusCatalogo)
        {
            if (id != statusCatalogo.IdStatus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusCatalogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusCatalogoExists(statusCatalogo.IdStatus))
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
            return View(statusCatalogo);
        }

        // GET: StatusCatalogo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusCatalogo = await _context.StatusCatalogos
                .FirstOrDefaultAsync(m => m.IdStatus == id);
            if (statusCatalogo == null)
            {
                return NotFound();
            }

            return View(statusCatalogo);
        }

        // POST: StatusCatalogo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusCatalogo = await _context.StatusCatalogos.FindAsync(id);
            if (statusCatalogo != null)
            {
                _context.StatusCatalogos.Remove(statusCatalogo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusCatalogoExists(int id)
        {
            return _context.StatusCatalogos.Any(e => e.IdStatus == id);
        }
    }
}
