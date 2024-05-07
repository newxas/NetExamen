using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenNet.Models
{
    public class Productos
    {
        [Key] 
        public int ID_Producto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public double Precio { get; set; }
        public double Impuesto { get; set; }

        public int ID_Sucursal { get; set; }
        [ForeignKey("ID_Sucursal")]
        public Sucursal? Sucursal { get; set; }

    }
}
