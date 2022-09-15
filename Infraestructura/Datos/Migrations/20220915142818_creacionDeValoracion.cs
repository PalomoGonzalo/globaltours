using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Datos.Migrations
{
    public partial class creacionDeValoracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Valoracion",
                table: "Lugar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valoracion",
                table: "Lugar");
        }
    }
}
