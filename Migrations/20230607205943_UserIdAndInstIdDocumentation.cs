using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class UserIdAndInstIdDocumentation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Documentations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Documentations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documentations_InstitutionId",
                table: "Documentations",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentations_UserId",
                table: "Documentations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentations_Institution_InstitutionId",
                table: "Documentations",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documentations_User_UserId",
                table: "Documentations",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentations_Institution_InstitutionId",
                table: "Documentations");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentations_User_UserId",
                table: "Documentations");

            migrationBuilder.DropIndex(
                name: "IX_Documentations_InstitutionId",
                table: "Documentations");

            migrationBuilder.DropIndex(
                name: "IX_Documentations_UserId",
                table: "Documentations");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Documentations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Documentations");
        }
    }
}
