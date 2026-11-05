// Importacion de frameworks y carpeta de modelos necesarios para el contexto de la base de datos
using Microsoft.EntityFrameworkCore;
using WebApplicationApi.Models;

namespace WebApplicationApi.Data
{
    // Clase de contexto de la base de datos, hereda de DbContext
    public class ApplicationDbContext : DbContext
    {
        // Constructor que recibe opciones de configuración y las pasa a la clase base DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        // Propiedad que representa la tabla de categorías en la base de datos
        public DbSet<Category> Categories { get; set; }
        // Propiedad que representa la tabla de productos en la base de datos
        public DbSet<Product> Products { get; set; }
    }
}
