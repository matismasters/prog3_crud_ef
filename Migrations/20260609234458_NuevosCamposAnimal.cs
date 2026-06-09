using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudEf.Migrations
{
    /// <inheritdoc />
    public partial class NuevosCamposAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaVivo",
                table: "Animales",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMuerte",
                table: "Animales",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Animales",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EstaVivo", "FechaMuerte" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Animales",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EstaVivo", "FechaMuerte" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Animales",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EstaVivo", "FechaMuerte" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Animales",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EstaVivo", "FechaMuerte" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaVivo",
                table: "Animales");

            migrationBuilder.DropColumn(
                name: "FechaMuerte",
                table: "Animales");
        }
    }
}
