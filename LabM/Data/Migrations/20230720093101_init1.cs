using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabM.Data.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Management", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalOrResidenceId = table.Column<int>(type: "int", nullable: false),
                    UniversityNumber = table.Column<int>(type: "int", nullable: false),
                    StudentsStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    College = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandFatherNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandFatherNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalFileNo = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Management");

            migrationBuilder.DropTable(
                name: "Request");
        }
    }
}
