using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenNet.Models
{
    public class Clientes
    {
        [Key] 
        public int ID_Cliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string TipoCliente { get; set; } = string.Empty;
        public int Descuento { get; set; }
        public int ID_Usuarios { get; set; }

        [ForeignKey("ID_Usuarios")]
        public Usuarios? Usuarios { get; set; }
    }
}
