using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class CompanyNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                schema: "setur",
                table: "persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                schema: "setur",
                table: "persons");
        }
    }
}
