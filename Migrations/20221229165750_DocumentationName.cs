using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class DocumentationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentations_Associations_AssociationsId",
                table: "Documentations");

            migrationBuilder.DropIndex(
                name: "IX_Documentations_AssociationsId",
                table: "Documentations");

            migrationBuilder.DropColumn(
                name: "AssociationsId",
                table: "Documentations");

            migrationBuilder.AlterColumn<string>(
                name: "doc_uri",
                table: "Documentations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DocName",
                table: "Documentations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocName",
                table: "Documentations");

            migrationBuilder.AlterColumn<string>(
                name: "doc_uri",
                table: "Documentations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssociationsId",
                table: "Documentations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documentations_AssociationsId",
                table: "Documentations",
                column: "AssociationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentations_Associations_AssociationsId",
                table: "Documentations",
                column: "AssociationsId",
                principalTable: "Associations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
