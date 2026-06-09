using Microsoft.EntityFrameworkCore;
using CrudEf.Models;

namespace CrudEf.Data
{
    // El DbContext es el corazon de Entity Framework: representa la sesion con
    // la base y expone cada entidad como un DbSet. A diferencia del repositorio
    // a mano (proyecto crud_ado), aca NO escribimos SQL ni mapeamos filas a
    // objetos: EF se encarga de eso a partir de esta configuracion.
    public class SafariContext : DbContext
    {
        public SafariContext(DbContextOptions<SafariContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animales { get; set; }

        // EF mapea las propiedades de Animal a columnas POR CONVENCION: con solo
        // declarar el DbSet de arriba ya tenemos tabla 'Animales' con columnas
        // Id, Especie, Sexo, Reserva, Energia y FechaAlta. No hace falta listar
        // las propiedades una por una.
        //
        // Aca abajo queda SOLO lo que EF no puede deducir solo (las tres cosas
        // que no tienen equivalente como data annotation en el modelo):
        //   1. Sexo es un 'char': le pedimos que lo guarde como texto ('M'/'H').
        //   2. FechaAlta arranca con el valor por defecto de la base.
        //   3. El seed de ejemplo, que viaja DENTRO de la migracion.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(animal =>
            {
                animal.Property(a => a.Sexo).HasConversion<string>();

                animal.Property(a => a.FechaAlta)
                      .HasDefaultValueSql("datetime('now')");

                animal.HasData(
                    new Animal { Id = 1, Especie = "leon",     Sexo = 'M', Reserva = "serengeti",  Energia = 80, FechaAlta = new DateTime(2026, 1, 1) },
                    new Animal { Id = 2, Especie = "leon",     Sexo = 'H', Reserva = "masai_mara", Energia = 75, FechaAlta = new DateTime(2026, 1, 1) },
                    new Animal { Id = 3, Especie = "tortuga",  Sexo = 'H', Reserva = "galapagos",  Energia = 40, FechaAlta = new DateTime(2026, 1, 1) },
                    new Animal { Id = 4, Especie = "pinguino", Sexo = 'M', Reserva = "patagonia",  Energia = 60, FechaAlta = new DateTime(2026, 1, 1) }
                );
            });
        }
    }
}
