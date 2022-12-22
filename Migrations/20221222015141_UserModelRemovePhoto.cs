using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class UserModelRemovePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "photo_uri",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_uri",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
