using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenNet.Models
{
    public class Usuarios
    {
        [Key] 
        public int ID_Usuarios { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;
        public string PasswordUsuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        public int ID_Sucursal { get; set; }

        [ForeignKey("ID_Sucursal")]
        public Sucursal? Sucursal { get; set; }

    }
}
