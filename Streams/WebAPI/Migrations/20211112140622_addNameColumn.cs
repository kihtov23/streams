using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class addNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeInMegabytes",
                table: "Articles");

            migrationBuilder.AddColumn<double>(
                name: "Name",
                table: "Articles",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Articles");

            migrationBuilder.AddColumn<double>(
                name: "SizeInMegabytes",
                table: "Articles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
