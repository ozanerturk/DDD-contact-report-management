using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Report_Status_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reports_status_StatusId",
                schema: "ddd",
                table: "reports");

            migrationBuilder.DropTable(
                name: "status",
                schema: "ddd");

            migrationBuilder.DropIndex(
                name: "IX_reports_StatusId",
                schema: "ddd",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "ddd",
                table: "reports");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "ddd",
                table: "reports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ddd",
                table: "reports");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                schema: "ddd",
                table: "reports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "status",
                schema: "ddd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reports_StatusId",
                schema: "ddd",
                table: "reports",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_reports_status_StatusId",
                schema: "ddd",
                table: "reports",
                column: "StatusId",
                principalSchema: "ddd",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
