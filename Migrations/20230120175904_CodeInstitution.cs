using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class CodeInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "CodeInstitutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeInstitutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeInstitutions_Institution_InstitutionModelId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliations_UserId",
                table: "Avaliations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeInstitutions_InstitutionModelId",
                table: "CodeInstitutions",
                column: "InstitutionId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliations_Institution_InstitutionId",
                table: "Avaliations");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliations_User_UserId",
                table: "Avaliations");

            migrationBuilder.DropTable(
                name: "CodeInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_Avaliations_InstitutionId",
                table: "Avaliations");

            migrationBuilder.DropIndex(
                name: "IX_Avaliations_UserId",
                table: "Avaliations");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Avaliations");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Avaliations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Avaliations");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Avaliations",
                newName: "NoteAvaliation");

            migrationBuilder.AddColumn<string>(
                name: "CommentAvaliation",
                table: "Avaliations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
