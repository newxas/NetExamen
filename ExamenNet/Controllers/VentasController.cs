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
    public class VentasController : Controller
    {
        private readonly ContextModel _context;

        public VentasController(ContextModel context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var contextModel = _context.Ventas.Include(v => v.Clientes).Include(v => v.Productos).Include(v => v.Promociones).Include(v => v.Usuarios);
            return View(await contextModel.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .Include(v => v.Clientes)
                .Include(v => v.Productos)
                .Include(v => v.Promociones)
                .Include(v => v.Usuarios)
                .FirstOrDefaultAsync(m => m.ID_venta == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["FechaVenta"] = DateTime.Now.ToString("MM/dd/yyyy");
            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "ID_Cliente", "NombreCliente");
            ViewData["ID_Producto"] = new SelectList(_context.Productos, "ID_Producto", "NombreProducto");
            ViewData["ID_Promocion"] = new SelectList(_context.Promociones, "ID_Promocion", "NombrePromocion");
            ViewData["ID_Usuarios"] = new SelectList(_context.Usuarios.Where(x=>x.Rol == "Asesor de Venta"), "ID_Usuarios", "NombreUsuario");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_venta,TipoVenta,FechaVenta,SubTotal,Total,ID_Promocion,ID_Producto,ID_Usuarios,ID_Cliente")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "ID_Cliente", "ID_Cliente", ventas.ID_Cliente);
            ViewData["ID_Producto"] = new SelectList(_context.Productos, "ID_Producto", "ID_Producto", ventas.ID_Producto);
            ViewData["ID_Promocion"] = new SelectList(_context.Promociones, "ID_Promocion", "ID_Promocion", ventas.ID_Promocion);
            ViewData["ID_Usuarios"] = new SelectList(_context.Usuarios, "ID_Usuarios", "ID_Usuarios", ventas.ID_Usuarios);
            return View(ventas);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas.FindAsync(id);
            if (ventas == null)
            {
                return NotFound();
            }
            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "ID_Cliente", "NombreCliente", ventas.ID_Cliente);
            ViewData["ID_Producto"] = new SelectList(_context.Productos, "ID_Producto", "NombreProducto", ventas.ID_Producto);
            ViewData["ID_Promocion"] = new SelectList(_context.Promociones, "ID_Promocion", "NombrePromocion", ventas.ID_Promocion);
            ViewData["ID_Usuarios"] = new SelectList(_context.Usuarios.Where(y => y.Rol == "Asesor de Venta"), "ID_Usuarios", "NombreUsuario", ventas.ID_Usuarios);
            return View(ventas);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_venta,TipoVenta,FechaVenta,SubTotal,Total,ID_Promocion,ID_Producto,ID_Usuarios,ID_Cliente")] Ventas ventas)
        {
            if (id != ventas.ID_venta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasExists(ventas.ID_venta))
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
            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "ID_Cliente", "ID_Cliente", ventas.ID_Cliente);
            ViewData["ID_Producto"] = new SelectList(_context.Productos, "ID_Producto", "ID_Producto", ventas.ID_Producto);
            ViewData["ID_Promocion"] = new SelectList(_context.Promociones, "ID_Promocion", "ID_Promocion", ventas.ID_Promocion);
            ViewData["ID_Usuarios"] = new SelectList(_context.Usuarios, "ID_Usuarios", "ID_Usuarios", ventas.ID_Usuarios);
            return View(ventas);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .Include(v => v.Clientes)
                .Include(v => v.Productos)
                .Include(v => v.Promociones)
                .Include(v => v.Usuarios)
                .FirstOrDefaultAsync(m => m.ID_venta == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'ContextModel.Ventas'  is null.");
            }
            var ventas = await _context.Ventas.FindAsync(id);
            if (ventas != null)
            {
                _context.Ventas.Remove(ventas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasExists(int id)
        {
          return (_context.Ventas?.Any(e => e.ID_venta == id)).GetValueOrDefault();
        }

        //Menu
        public IActionResult Menu()
        {
            return View();
        }


        //JSON
        [HttpPost]
        public IActionResult ConsultaInfo(int idProduct)
        {
            var consultaPrecio = _context.Productos.Where(y=>y.ID_Producto == idProduct).FirstOrDefault().Precio;
            var consultaImpuesto = _context.Productos.Where(y => y.ID_Producto == idProduct).FirstOrDefault().Impuesto;
            double PrecioTotal = consultaPrecio + consultaImpuesto;
            if (consultaPrecio > 0)
            {
                return Json( PrecioTotal);
            }
            else
            {
                return Json(new { success = false });
            }            
        }

        [HttpPost]
        public IActionResult ConsultaProm(int idProm, double subTotal)
        {
            if(subTotal > 0)
            {
                var consultaPromo = _context.Promociones.Where(y => y.ID_Promocion == idProm).FirstOrDefault().TotalPromocion;
                
                if (consultaPromo > 0)
                {
                    var CalculoPromo = (Convert.ToDouble(consultaPromo) * subTotal) / 100;
                    double PrecioTotal = subTotal - CalculoPromo;
                    return Json(PrecioTotal);
                }
                else
                {
                    return Json(subTotal);
                }
            }
            else
            {
                return Json(new { success = false });
            }               
        }
    }
}
