using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class InstitutionWithCep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Institution",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Institution");
        }
    }
}
