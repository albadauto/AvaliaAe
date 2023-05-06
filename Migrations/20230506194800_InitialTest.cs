using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaliaAe.Migrations
{
    public partial class InitialTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstitutionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    photo_uri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_allowed = table.Column<bool>(type: "bit", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionTypeId = table.Column<int>(type: "int", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerCpf = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Institution_InstitutionType_InstitutionTypeId",
                        column: x => x.InstitutionTypeId,
                        principalTable: "InstitutionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Code", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Code_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Associations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Associations_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Associations_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Avaliations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    AverageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliations_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliations_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeCertification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certification_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_CodeInstitutions_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documentations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doc_uri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssociationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentations_Associations_AssociationsId",
                        column: x => x.AssociationsId,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Average",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Average = table.Column<double>(type: "float", nullable: true),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    AvaliationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Average", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Average_Avaliations_AvaliationId",
                        column: x => x.AvaliationId,
                        principalTable: "Avaliations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Average_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Denounces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvaliationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denounces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Denounces_Avaliations_AvaliationId",
                        column: x => x.AvaliationId,
                        principalTable: "Avaliations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Associations_InstitutionId",
                table: "Associations",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_UserId",
                table: "Associations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliations_InstitutionId",
                table: "Avaliations",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliations_UserId",
                table: "Avaliations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Average_AvaliationId",
                table: "Average",
                column: "AvaliationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Average_InstitutionId",
                table: "Average",
                column: "InstitutionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certification_InstitutionId",
                table: "Certification",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Code_UserId",
                table: "Code",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeInstitutions_InstitutionId",
                table: "CodeInstitutions",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_AvaliationId",
                table: "Denounces",
                column: "AvaliationId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentations_AssociationsId",
                table: "Documentations",
                column: "AssociationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Institution_InstitutionTypeId",
                table: "Institution",
                column: "InstitutionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Average");

            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "Code");

            migrationBuilder.DropTable(
                name: "CodeInstitutions");

            migrationBuilder.DropTable(
                name: "Denounces");

            migrationBuilder.DropTable(
                name: "Documentations");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropTable(
                name: "Avaliations");

            migrationBuilder.DropTable(
                name: "Associations");

            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "InstitutionType");
        }
    }
}
