using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class CodeInstitution_V2 : Migration
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
                        name: "InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

      
            migrationBuilder.CreateIndex(
                name: "IX_CodeInstitutions_InstitutionModelId",
                table: "CodeInstitutions",
                column: "InstitutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
