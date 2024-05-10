using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenNet.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExamenNet.Controllers
{
    [Authorize]
    public class PromocionesController : Controller
    {
        private readonly ContextModel _context;

        public PromocionesController(ContextModel context)
        {
            _context = context;
        }

        // GET: Promociones
        public async Task<IActionResult> Index()
        {
              return _context.Promociones != null ? 
                          View(await _context.Promociones.ToListAsync()) :
                          Problem("Entity set 'ContextModel.Promociones'  is null.");
        }

        // GET: Promociones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promociones = await _context.Promociones
                .FirstOrDefaultAsync(m => m.ID_Promocion == id);
            if (promociones == null)
            {
                return NotFound();
            }

            return View(promociones);
        }

        // GET: Promociones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promociones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Promocion,NombrePromocion,TotalPromocion")] Promociones promociones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promociones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promociones);
        }

        // GET: Promociones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promociones = await _context.Promociones.FindAsync(id);
            if (promociones == null)
            {
                return NotFound();
            }
            return View(promociones);
        }

        // POST: Promociones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Promocion,NombrePromocion,TotalPromocion")] Promociones promociones)
        {
            if (id != promociones.ID_Promocion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promociones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocionesExists(promociones.ID_Promocion))
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
            return View(promociones);
        }

        // GET: Promociones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promociones = await _context.Promociones
                .FirstOrDefaultAsync(m => m.ID_Promocion == id);
            if (promociones == null)
            {
                return NotFound();
            }

            return View(promociones);
        }

        // POST: Promociones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Promociones == null)
            {
                return Problem("Entity set 'ContextModel.Promociones'  is null.");
            }
            var promociones = await _context.Promociones.FindAsync(id);
            if (promociones != null)
            {
                _context.Promociones.Remove(promociones);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocionesExists(int id)
        {
          return (_context.Promociones?.Any(e => e.ID_Promocion == id)).GetValueOrDefault();
        }

        public IActionResult Menu()
        {
            return View();
        }
    }
}
