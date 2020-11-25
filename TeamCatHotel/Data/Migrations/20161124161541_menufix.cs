using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCatHotel.Data.Migrations
{
    public partial class menufix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "horario",
                table: "Servicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "horaFin",
                table: "Servicio",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "horaInicio",
                table: "Servicio",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "telefono",
                table: "Persona",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "horaFin",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "horaInicio",
                table: "Servicio");

            migrationBuilder.AddColumn<int>(
                name: "horario",
                table: "Servicio",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "telefono",
                table: "Persona",
                nullable: false);
        }
    }
}
