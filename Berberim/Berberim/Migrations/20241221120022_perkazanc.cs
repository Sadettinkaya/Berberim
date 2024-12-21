using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Berberim.Migrations
{
    public partial class perkazanc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kazancs",
                columns: table => new
                {
                    PersonelKazancID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonnelID = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalPersonelKazanc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kazancs", x => x.PersonelKazancID);
                    table.ForeignKey(
                        name: "FK_kazancs_personnels_PersonnelID",
                        column: x => x.PersonnelID,
                        principalTable: "personnels",
                        principalColumn: "personelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kazancs_PersonnelID",
                table: "kazancs",
                column: "PersonnelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kazancs");
        }
    }
}
