﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "setur");

            migrationBuilder.CreateSequence(
                name: "statisticseq",
                schema: "setur",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "statistics",
                schema: "setur",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    Location = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "statistics",
                schema: "setur");

            migrationBuilder.DropSequence(
                name: "statisticseq",
                schema: "setur");
        }
    }
}
