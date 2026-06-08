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

        // Aca le decimos a EF como se llama la tabla y las columnas, para que el
        // schema generado por las migraciones coincida con el del proyecto a
        // mano. Tambien sembramos los datos de ejemplo: el seed viaja DENTRO de
        // la migracion, asi que la base nace poblada y versionada.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(animal =>
            {
                animal.ToTable("animales");

                animal.Property(a => a.Id).HasColumnName("id");
                animal.Property(a => a.Especie).HasColumnName("especie").IsRequired();
                animal.Property(a => a.Sexo).HasColumnName("sexo").HasConversion<string>().IsRequired();
                animal.Property(a => a.Reserva).HasColumnName("reserva").IsRequired();
                animal.Property(a => a.Energia).HasColumnName("energia").IsRequired();

                animal.Property(a => a.FechaAlta)
                      .HasColumnName("fecha_alta")
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
