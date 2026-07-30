using System.ComponentModel.DataAnnotations;

namespace SistemaTelefonico.Models;

public class Telefono
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del responsable es obligatorio")]
    [StringLength(100)]
    public string NombreDueno { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número es obligatorio")]
    [StringLength(4, MinimumLength = 4, ErrorMessage = "El número debe tener 4 caracteres")]
    public string NumeroTelefono { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }
}