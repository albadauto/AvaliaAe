using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class FixingModelDenounce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denounces_Institution_InstitutionId",
                table: "Denounces");

            migrationBuilder.DropForeignKey(
                name: "FK_Denounces_User_UserId",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Denounces_InstitutionId",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Denounces");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Denounces",
                newName: "AvaliationId");

            migrationBuilder.RenameIndex(
                name: "IX_Denounces_UserId",
                table: "Denounces",
                newName: "IX_Denounces_AvaliationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Denounces_Avaliations_AvaliationId",
                table: "Denounces",
                column: "AvaliationId",
                principalTable: "Avaliations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denounces_Avaliations_AvaliationId",
                table: "Denounces");

            migrationBuilder.RenameColumn(
                name: "AvaliationId",
                table: "Denounces",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Denounces_AvaliationId",
                table: "Denounces",
                newName: "IX_Denounces_UserId");

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Denounces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_InstitutionId",
                table: "Denounces",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Denounces_Institution_InstitutionId",
                table: "Denounces",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Denounces_User_UserId",
                table: "Denounces",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
