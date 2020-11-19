using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ddd");

            migrationBuilder.CreateSequence(
                name: "personseq",
                schema: "ddd",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "persons",
                schema: "ddd",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    IdentityGuid = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contactInformations",
                schema: "ddd",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Phone_PhoneNumber = table.Column<string>(nullable: true),
                    Email_EmailAddress = table.Column<string>(nullable: true),
                    Desciption = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contactInformations_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "ddd",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contactInformations_PersonId",
                schema: "ddd",
                table: "contactInformations",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contactInformations",
                schema: "ddd");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "ddd");

            migrationBuilder.DropSequence(
                name: "personseq",
                schema: "ddd");
        }
    }
}
