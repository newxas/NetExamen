using System.ComponentModel.DataAnnotations;

namespace ExamenNet.Models
{
    public class Promociones
    {
        [Key] 
        public int ID_Promocion { get; set; }
        public string NombrePromocion { get; set; } = string.Empty;
        public int TotalPromocion { get; set; }

    }
}
