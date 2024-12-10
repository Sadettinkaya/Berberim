using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Berberim.Migrations
{
    public partial class AppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "adSoyad",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Salon",
                columns: table => new
                {
                    salonID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    salonName = table.Column<string>(type: "text", nullable: true),
                    salonType = table.Column<string>(type: "text", nullable: true),
                    workingHours = table.Column<string>(type: "text", nullable: true),
                    salonAddress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salon", x => x.salonID);
                });

            migrationBuilder.CreateTable(
                name: "Uzmanlik",
                columns: table => new
                {
                    UzmanlikID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UzmanlikName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzmanlik", x => x.UzmanlikID);
                });

            migrationBuilder.CreateTable(
                name: "Hizmet",
                columns: table => new
                {
                    hizmetID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hizmetName = table.Column<string>(type: "text", nullable: true),
                    hizmetDuration = table.Column<double>(type: "double precision", nullable: false),
                    hizmetPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    salonID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmet", x => x.hizmetID);
                    table.ForeignKey(
                        name: "FK_Hizmet_Salon_salonID",
                        column: x => x.salonID,
                        principalTable: "Salon",
                        principalColumn: "salonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personel",
                columns: table => new
                {
                    personelID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    personelName = table.Column<string>(type: "text", nullable: true),
                    personelPassword = table.Column<string>(type: "text", nullable: true),
                    personelEmail = table.Column<string>(type: "text", nullable: true),
                    musaitSaat = table.Column<string>(type: "text", nullable: true),
                    salonID = table.Column<int>(type: "integer", nullable: false),
                    UzmanlikID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personel", x => x.personelID);
                    table.ForeignKey(
                        name: "FK_Personel_Salon_salonID",
                        column: x => x.salonID,
                        principalTable: "Salon",
                        principalColumn: "salonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personel_Uzmanlik_UzmanlikID",
                        column: x => x.UzmanlikID,
                        principalTable: "Uzmanlik",
                        principalColumn: "UzmanlikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevu",
                columns: table => new
                {
                    randevuID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    randevuTarih = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    randevuSaat = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MusteriID = table.Column<int>(type: "integer", nullable: false),
                    MusteriName = table.Column<string>(type: "text", nullable: false),
                    personelID = table.Column<int>(type: "integer", nullable: false),
                    hizmetID = table.Column<int>(type: "integer", nullable: false),
                    hizmetName = table.Column<string>(type: "text", nullable: false),
                    onaylandimi = table.Column<bool>(type: "boolean", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: false),
                    tel = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevu", x => x.randevuID);
                    table.ForeignKey(
                        name: "FK_Randevu_AspNetUsers_MusteriID",
                        column: x => x.MusteriID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevu_Hizmet_hizmetID",
                        column: x => x.hizmetID,
                        principalTable: "Hizmet",
                        principalColumn: "hizmetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevu_Personel_personelID",
                        column: x => x.personelID,
                        principalTable: "Personel",
                        principalColumn: "personelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hizmet_salonID",
                table: "Hizmet",
                column: "salonID");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_salonID",
                table: "Personel",
                column: "salonID");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_UzmanlikID",
                table: "Personel",
                column: "UzmanlikID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_hizmetID",
                table: "Randevu",
                column: "hizmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_MusteriID",
                table: "Randevu",
                column: "MusteriID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_personelID",
                table: "Randevu",
                column: "personelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevu");

            migrationBuilder.DropTable(
                name: "Hizmet");

            migrationBuilder.DropTable(
                name: "Personel");

            migrationBuilder.DropTable(
                name: "Salon");

            migrationBuilder.DropTable(
                name: "Uzmanlik");

            migrationBuilder.DropColumn(
                name: "adSoyad",
                table: "AspNetUsers");
        }
    }
}
