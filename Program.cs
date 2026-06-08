using Microsoft.EntityFrameworkCore;
using CrudEf.Data;

// El template arranca con 'var'; en el curso escribimos tipos explicitos.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registramos el DbContext de EF apuntando a SQLite. No registramos ningun
// repositorio: el DbContext ya es el repositorio, y el controller lo usa directo.
string? cs = builder.Configuration.GetConnectionString("Animales");
builder.Services.AddDbContext<SafariContext>(opciones => opciones.UseSqlite(cs));

WebApplication app = builder.Build();

// Al arrancar, aplicamos las migraciones pendientes. Esto crea la base y la
// tabla (con el seed) si no existen, y aplica cualquier cambio de schema nuevo.
// Es el reemplazo del schema.sql a mano: el schema lo define EF a partir de la
// clase Animal y queda versionado en la carpeta Migrations.
using (IServiceScope scope = app.Services.CreateScope())
{
    SafariContext context = scope.ServiceProvider.GetRequiredService<SafariContext>();
    context.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// La pantalla de entrada del proyecto es el CRUD de animales.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Animales}/{action=Index}/{id?}");

app.Run();
