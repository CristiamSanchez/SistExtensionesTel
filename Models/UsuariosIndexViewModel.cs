using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaTelefonico.Models;

public class UsuariosIndexViewModel
{
    public List<UsuarioItemViewModel> Usuarios { get; set; } = new();
    public CrearUsuarioViewModel NuevoUsuario { get; set; } = new();
    public CambiarPasswordViewModel CambiarPassword { get; set; } = new();
}

public class UsuarioItemViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = new List<string>();
}

public class CrearUsuarioViewModel
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de correo no válido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme la contraseña")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Seleccione un rol")]
    public string Rol { get; set; } = "Lectura";
}

public class CambiarPasswordViewModel
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    public string? Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme la contraseña")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
