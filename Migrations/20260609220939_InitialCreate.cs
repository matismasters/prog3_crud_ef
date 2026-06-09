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
                name: "Animales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Especie = table.Column<string>(type: "TEXT", nullable: false),
                    Sexo = table.Column<string>(type: "TEXT", nullable: false),
                    Reserva = table.Column<string>(type: "TEXT", nullable: false),
                    Energia = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animales", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Animales",
                columns: new[] { "Id", "Energia", "Especie", "FechaAlta", "Reserva", "Sexo" },
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
                name: "Animales");
        }
    }
}
