using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ExamenNet.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ContextModel _context;

        public UsuariosController(ContextModel context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var contextModel = _context.Usuarios.Include(u => u.Sucursal);
            return View(await contextModel.ToListAsync());
        }

        // GET: Usuarios/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.Sucursal)
                .FirstOrDefaultAsync(m => m.ID_Usuarios == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // GET: Usuarios/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ID_Sucursal"] = new SelectList(_context.Sucursal, "ID_Sucursal", "NombreSucursal");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Usuarios,NombreUsuario,PasswordUsuario,Rol,ID_Sucursal")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Sucursal"] = new SelectList(_context.Sucursal, "ID_Sucursal", "ID_Sucursal", usuarios.ID_Sucursal);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            ViewData["ID_Sucursal"] = new SelectList(_context.Sucursal, "ID_Sucursal", "NombreSucursal", usuarios.ID_Sucursal);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Usuarios,NombreUsuario,PasswordUsuario,Rol,ID_Sucursal")] Usuarios usuarios)
        {
            if (id != usuarios.ID_Usuarios)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.ID_Usuarios))
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
            ViewData["ID_Sucursal"] = new SelectList(_context.Sucursal, "ID_Sucursal", "ID_Sucursal", usuarios.ID_Sucursal);
            return View(usuarios);
        }

        [Authorize]
        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.Sucursal)
                .FirstOrDefaultAsync(m => m.ID_Usuarios == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        [Authorize]
        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'ContextModel.Usuarios'  is null.");
            }
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios != null)
            {
                _context.Usuarios.Remove(usuarios);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.ID_Usuarios == id)).GetValueOrDefault();
        }

        //Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]    
        public async Task<IActionResult> Login(Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                var User = usuarios.NombreUsuario;
                var Password = usuarios.PasswordUsuario;

                var consulta = _context.Usuarios.Where(b => b.NombreUsuario == User && b.PasswordUsuario == Password).FirstOrDefault();

                if (consulta != null)
                {
                    var claims = new List<Claim> {

                        new Claim(ClaimTypes.Name, consulta.NombreUsuario),
                        new Claim(ClaimTypes.Role, consulta.Rol)
                    };

                    var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndentity));
                    return RedirectToAction("Index", "Usuarios");
                }

            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
