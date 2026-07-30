using System.Collections.Generic;

namespace SistemaTelefonico.Models
{
    public class TelefonosIndexViewModel
    {
        // Lista de teléfonos para mostrar en el grid
        public IEnumerable<Telefono> Telefonos { get; set; } = new List<Telefono>();

        // Objeto vacío para el modal de agregar
        public Telefono NuevoTelefono { get; set; } = new Telefono();
    }
}