using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class AvaliationsCorrectionsTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Avaliations",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Note",
                table: "Avaliations",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoteAvaliation",
                table: "Avaliations",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "CommentAvaliation",
                table: "Avaliations",
                newName: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Avaliations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Avaliations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Avaliations_InstitutionId",
                table: "Avaliations",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliations_UserId",
                table: "Avaliations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliations_Institution_InstitutionId",
                table: "Avaliations",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliations_User_UserId",
                table: "Avaliations",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
