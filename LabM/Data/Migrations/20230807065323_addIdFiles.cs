using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabM.Data.Migrations
{
    public partial class addIdFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResidenceIdName",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentIdName",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResidenceIdName",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "StudentIdName",
                table: "Request");
        }
    }
}
