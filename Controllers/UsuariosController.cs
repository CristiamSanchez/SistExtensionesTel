using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaTelefonico.Models;

namespace SistemaTelefonico.Controllers;

[Authorize(Roles = "Administrador")]
public class UsuariosController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsuariosController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var vm = await BuildViewModelAsync();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CrearUsuarioViewModel nuevoUsuario)
    {
        if (nuevoUsuario.Rol != "Administrador" && nuevoUsuario.Rol != "Lectura")
        {
            ModelState.AddModelError(nameof(nuevoUsuario.Rol), "Rol no válido");
        }

        if (!await _roleManager.RoleExistsAsync(nuevoUsuario.Rol))
        {
            ModelState.AddModelError(nameof(nuevoUsuario.Rol), "El rol seleccionado no existe");
        }

        var existingUser = await _userManager.FindByEmailAsync(nuevoUsuario.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError(nameof(nuevoUsuario.Email), "Ese correo ya está registrado");
        }

        if (!ModelState.IsValid)
        {
            var invalidVm = await BuildViewModelAsync(nuevoUsuario);
            return View("Index", invalidVm);
        }

        var user = new IdentityUser
        {
            UserName = nuevoUsuario.Email,
            Email = nuevoUsuario.Email,
            EmailConfirmed = true
        };

        var createResult = await _userManager.CreateAsync(user, nuevoUsuario.Password);
        if (!createResult.Succeeded)
        {
            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            var invalidVm = await BuildViewModelAsync(nuevoUsuario);
            return View("Index", invalidVm);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, nuevoUsuario.Rol);
        if (!roleResult.Succeeded)
        {
            foreach (var error in roleResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await _userManager.DeleteAsync(user);
            var invalidVm = await BuildViewModelAsync(nuevoUsuario);
            return View("Index", invalidVm);
        }

        TempData["SuccessMessage"] = "Usuario creado correctamente";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarPassword([Bind(Prefix = "CambiarPassword")] CambiarPasswordViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "No se pudo identificar el usuario a modificar.");
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            var invalidVm = await BuildViewModelAsync();
            invalidVm.CambiarPassword = model;
            TempData["OpenChangePasswordModal"] = "true";
            return View("Index", invalidVm);
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            var invalidVm = await BuildViewModelAsync();
            invalidVm.CambiarPassword = model;
            TempData["OpenChangePasswordModal"] = "true";
            return View("Index", invalidVm);
        }

        TempData["SuccessMessage"] = "Contraseña actualizada correctamente";
        return RedirectToAction(nameof(Index));
    }

    private async Task<UsuariosIndexViewModel> BuildViewModelAsync(CrearUsuarioViewModel? nuevoUsuario = null)
    {
        var users = _userManager.Users.ToList();
        var usuarios = new List<UsuarioItemViewModel>();

        foreach (var user in users.OrderBy(u => u.Email))
        {
            var roles = await _userManager.GetRolesAsync(user);
            usuarios.Add(new UsuarioItemViewModel
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                UserName = user.UserName ?? string.Empty,
                Roles = roles
            });
        }

        return new UsuariosIndexViewModel
        {
            Usuarios = usuarios,
            NuevoUsuario = nuevoUsuario ?? new CrearUsuarioViewModel(),
            CambiarPassword = new CambiarPasswordViewModel()
        };
    }
}
