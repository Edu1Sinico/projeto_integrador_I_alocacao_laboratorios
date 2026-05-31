using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLocLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBlocoLaboratorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bloco",
                table: "Laboratorios",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bloco",
                table: "Laboratorios");
        }
    }
}
