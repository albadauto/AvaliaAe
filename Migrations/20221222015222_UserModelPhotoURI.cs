using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class UserModelPhotoURI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "photo",
                table: "User",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo",
                table: "User");
        }
    }
}
