using System.ComponentModel.DataAnnotations;

namespace ExamenNet.Models
{
    public class Sucursal
    {
        [Key]
        public int ID_Sucursal { get; set; }

        public string NombreSucursal { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
    }
}
