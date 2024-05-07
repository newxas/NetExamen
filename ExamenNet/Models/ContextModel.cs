using Microsoft.EntityFrameworkCore;
using ExamenNet.Models;

namespace ExamenNet.Models
{
    public class ContextModel : DbContext
    {
        public ContextModel(DbContextOptions<ContextModel> options) : base(options) 
        { 
        }

        public DbSet<Sucursal> Sucursal { get; set; }
        public DbSet<Promociones> Promociones { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Clientes> Clientes { get; set;}
    }
}
