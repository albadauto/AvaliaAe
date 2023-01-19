using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class AvaliationsCorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliations_Institution_InstitutionId",
                table: "Avaliations");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliations_User_UserId",
                table: "Avaliations");

            migrationBuilder.DropIndex(
                name: "IX_Avaliations_InstitutionId",
                table: "Avaliations");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Avaliations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Avaliations",
                newName: "associationsModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliations_UserId",
                table: "Avaliations",
                newName: "IX_Avaliations_associationsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliations_Associations_associationsModelId",
                table: "Avaliations",
                column: "associationsModelId",
                principalTable: "Associations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserType_UserTypeId",
                table: "User",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
