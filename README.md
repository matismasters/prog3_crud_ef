# CRUD con Entity Framework — EF Core + SQLite

Proyecto de la **Unidad 7** de Programación 3 (2026). Es el **mismo CRUD de
`animales`** que [`prog3_crud_ado`](https://github.com/matismasters/prog3_crud_ado),
con la misma UI, el mismo `Animal` y el mismo `AnimalFormDto`... pero ahora la
persistencia la maneja **Entity Framework Core** en vez de ADO.NET a mano.

La gracia es comparar los dos proyectos lado a lado: cambia **cómo se guardan
los datos**, y ahí se ve todo lo que EF te saca de encima.

## Qué resuelve EF

1. **El mapeo fila ↔ objeto desaparece.** En crud_ado había un `MapearAnimal`
   que copiaba columna por columna. Acá EF mapea solo, a partir de la
   configuración del `DbContext`.
2. **El schema lo manejan las migraciones.** Ya no hay un `schema.sql` a mano
   divorciado del código: el schema se genera desde la clase `Animal` con
   `dotnet ef migrations add`, queda versionado en la carpeta `Migrations/` y se
   aplica solo al arrancar (`context.Database.Migrate()`). Hasta el seed de
   datos viaja dentro de la migración (`HasData`).
3. **El boilerplate se evapora.** Las operaciones del CRUD pasan a ser una o dos
   líneas (`ToList`, `Find`, `Add` + `SaveChanges`, `Remove`). No hay conexión,
   ni comando, ni parámetros uno por uno.

## La diferencia más grande: no hay repositorio

`crud_ado` tiene un `AnimalRepository` que existe para **esconder la danza de
ADO.NET** (abrir conexión, comando, parámetros, mapear filas). En `crud_ef`
**ese repositorio no existe, y no hace falta**: el `DbContext` de EF **ya es** un
repositorio + una unidad de trabajo.

- `DbSet<Animal>` (la propiedad `Animales`) es el repositorio: te da `Add`,
  `Remove`, `Find` y consultas (`Where`, `OrderBy`, `ToList`).
- `SaveChanges()` es la unidad de trabajo: confirma todos los cambios juntos en
  una transacción.

Por eso el `AnimalesController` de `crud_ef` recibe el `SafariContext`
directamente y le habla a él. EF no achicó la capa de repositorio: la **eliminó**.

## Cómo correrlo

Necesitás el **SDK de .NET 8**.

```bash
dotnet run
```

Abrí la URL que imprime la consola. La base `animales.db` se crea sola
aplicando las migraciones, con la tabla y los datos de ejemplo.

## Trabajar con migraciones

```bash
# instalar la tool una vez
dotnet tool install --global dotnet-ef --version 8.0.*

# crear una migración después de cambiar la clase Animal o el DbContext
dotnet ef migrations add NombreDelCambio

# aplicar migraciones a la base (también se aplican solas al arrancar la app)
dotnet ef database update
```

## Estructura

| Pieza | Archivo | Qué hace |
|---|---|---|
| Entidad | `Models/Animal.cs` | La clase tipada (idéntica a crud_ado). |
| DTO | `Models/AnimalFormDto.cs` | Datos del formulario, con validación (idéntico). |
| DbContext | `Data/SafariContext.cs` | La sesión con la base; ES el repositorio + unidad de trabajo. |
| Controller | `Controllers/AnimalesController.cs` | Acciones MVC; habla directo con el DbContext. |
| Vistas | `Views/Animales/*.cshtml` | Index, Details, Create, Edit, Delete (idénticas). |
| Migraciones | `Migrations/*` | El schema versionado, generado por EF. |
