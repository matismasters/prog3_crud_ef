using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudEf.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "animales",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    especie = table.Column<string>(type: "TEXT", nullable: false),
                    sexo = table.Column<string>(type: "TEXT", nullable: false),
                    reserva = table.Column<string>(type: "TEXT", nullable: false),
                    energia = table.Column<int>(type: "INTEGER", nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animales", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "animales",
                columns: new[] { "id", "energia", "especie", "fecha_alta", "reserva", "sexo" },
                values: new object[,]
                {
                    { 1, 80, "leon", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "serengeti", "M" },
                    { 2, 75, "leon", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "masai_mara", "H" },
                    { 3, 40, "tortuga", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "galapagos", "H" },
                    { 4, 60, "pinguino", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "patagonia", "M" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animales");
        }
    }
}
