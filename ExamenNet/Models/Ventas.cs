using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenNet.Models
{
    public class Ventas
    {
        [Key]
        public int ID_venta { get; set; }
        public string TipoVenta { get; set; } = string.Empty;
        public string FechaVenta { get; set; } = string.Empty;
        public double SubTotal { get; set; }
        public double Total {  get; set; }
        public int ID_Promocion { get; set; }
        public int ID_Producto { get; set; }
        public int ID_Usuarios { get; set; }
        public int ID_Cliente {  get; set; }

        [ForeignKey("ID_Promocion")]
        public Promociones? Promociones { get; set;}

        [ForeignKey("ID_Producto")]
        public Productos? Productos { get; set;}

        [ForeignKey("ID_Usuarios")]
        public Usuarios? Usuarios { get; set;}

        [ForeignKey("ID_Cliente")]
        public Clientes? Clientes { get; set;}
    }
}
