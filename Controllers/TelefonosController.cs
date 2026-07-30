using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaTelefonico.Data;
using SistemaTelefonico.Models;
using Microsoft.AspNetCore.Authorization;

namespace SistemaTelefonico.Controllers
{

    [Authorize]
    public class TelefonosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TelefonosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Telefonos
        public async Task<IActionResult> Index()
        {
            var data = await _context.Telefonos.ToListAsync();
            var vm = new TelefonosIndexViewModel
            {
                Telefonos = data
            };
            return View(vm);
        }
        /* public async Task<IActionResult> Index()
        {
                var data = await _context.Telefonos.ToListAsync();
                return View(data);
            //return View(await _context.Telefonos.ToListAsync());
        } */

        // GET: Telefonos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: Telefonos/Create
        [Authorize(Roles = "Administrador")]     
        public IActionResult Create()
        {
            return View();
        }

        // POST: Telefonos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreDueno,NumeroTelefono,Descripcion,FechaCreacion")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                var numeroExistente = await _context.Telefonos
                    .AnyAsync(t => t.NumeroTelefono == telefono.NumeroTelefono);

                if (numeroExistente)
                {
                    return Json(new
                    {
                        success = false,
                        errors = new Dictionary<string, string>
                        {
                            [nameof(Telefono.NumeroTelefono)] = "El número telefónico ya existe."
                        }
                    });
                }

                telefono.UsuarioCreacion = User.Identity?.Name;
                telefono.FechaCreacion = DateTime.Now;

                try
                {
                    _context.Add(telefono);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (DbUpdateException)
                {
                    return Json(new
                    {
                        success = false,
                        errors = new Dictionary<string, string>
                        {
                            [nameof(Telefono.NumeroTelefono)] = "El número telefónico ya existe."
                        }
                    });
                }
            }

            var errors = ModelState
                .Where(x => x.Value!.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).First()
                );
            return Json(new { success = false, errors });
        }

        // GET: Telefonos/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // POST: Telefonos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreDueno,NumeroTelefono,Descripcion,FechaCreacion")] Telefono telefono)
        {
            if (id != telefono.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var telefonoDuplicado = await _context.Telefonos
                    .AnyAsync(t => t.NumeroTelefono == telefono.NumeroTelefono && t.Id != id);

                if (telefonoDuplicado)
                {
                    return Json(new
                    {
                        success = false,
                        errors = new Dictionary<string, string>
                        {
                            [nameof(Telefono.NumeroTelefono)] = "El número telefónico ya existe."
                        }
                    });
                }

                try
                {
                    var telefonoDb = await _context.Telefonos.FindAsync(id);
                    if (telefonoDb == null)
                    {
                        return NotFound();
                    }
                    telefonoDb.NombreDueno = telefono.NombreDueno;
                    telefonoDb.NumeroTelefono = telefono.NumeroTelefono;
                    telefonoDb.Descripcion = telefono.Descripcion;
                    telefonoDb.UsuarioModificacion = User.Identity?.Name;
                    telefonoDb.FechaModificacion = DateTime.Now;
                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(telefono.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    return Json(new
                    {
                        success = false,
                        errors = new Dictionary<string, string>
                        {
                            [nameof(Telefono.NumeroTelefono)] = "El número telefónico ya existe."
                        }
                    });
                }
                return Json(new { success = true });
            }
            var errors = ModelState
                .Where(x => x.Value!.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).First()
                );
            return Json(new { success = false, errors });
        }

        // GET: Telefonos/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: Telefonos/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelefonoExists(int id)
        {
            return _context.Telefonos.Any(e => e.Id == id);
        }
    }

}
