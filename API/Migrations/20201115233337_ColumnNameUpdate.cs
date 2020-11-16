using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class ColumnNameUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone_PhoneNumber",
                schema: "setur",
                table: "contactInformations",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email_EmailAddress",
                schema: "setur",
                table: "contactInformations",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "setur",
                table: "contactInformations",
                newName: "Phone_PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "setur",
                table: "contactInformations",
                newName: "Email_EmailAddress");
        }
    }
}
