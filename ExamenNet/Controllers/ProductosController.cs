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
    public class ProductosController : Controller
    {
        private readonly ContextModel _context;

        public ProductosController(ContextModel context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var contextModel = _context.Productos.Include(p => p.Sucursal);
            return View(await contextModel.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .Include(p => p.Sucursal)
                .FirstOrDefaultAsync(m => m.ID_Producto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["ID_Sucursal"] = new SelectList(_context.Sucursal, "ID_Sucursal", "ID_Sucursal");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Producto,NombreProducto,Precio,Impuesto,ID_Sucursal")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Sucursal"] = new SelectList(_context.Sucursal, "ID_Sucursal", "ID_Sucursal", productos.ID_Sucursal);
            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            ViewData["ID_Sucursal"] = new SelectList(_context.Sucursal, "ID_Sucursal", "NombreProducto", productos.ID_Sucursal);
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Producto,NombreProducto,Precio,Impuesto,ID_Sucursal")] Productos productos)
        {
            if (id != productos.ID_Producto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.ID_Producto))
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
            ViewData["ID_Sucursal"] = new SelectList(_context.Sucursal, "ID_Sucursal", "ID_Sucursal", productos.ID_Sucursal);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .Include(p => p.Sucursal)
                .FirstOrDefaultAsync(m => m.ID_Producto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'ContextModel.Productos'  is null.");
            }
            var productos = await _context.Productos.FindAsync(id);
            if (productos != null)
            {
                _context.Productos.Remove(productos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
          return (_context.Productos?.Any(e => e.ID_Producto == id)).GetValueOrDefault();
        }

        //Menu
        public IActionResult Menu()
        {
            return View();
        }
    }
}
