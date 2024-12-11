using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Berberim.Migrations
{
    public partial class iliskiler2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_AspNetUsers_MusteriID",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_personnels_personelID",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_services_hizmetID",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_personnels_expertises_UzmanlikID",
                table: "personnels");

            migrationBuilder.DropForeignKey(
                name: "FK_services_salons_salonID",
                table: "services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_services",
                table: "services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_expertises",
                table: "expertises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appointments",
                table: "appointments");

            migrationBuilder.RenameTable(
                name: "services",
                newName: "hizmets");

            migrationBuilder.RenameTable(
                name: "expertises",
                newName: "uzmanlıks");

            migrationBuilder.RenameTable(
                name: "appointments",
                newName: "randevus");

            migrationBuilder.RenameIndex(
                name: "IX_services_salonID",
                table: "hizmets",
                newName: "IX_hizmets_salonID");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_personelID",
                table: "randevus",
                newName: "IX_randevus_personelID");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_MusteriID",
                table: "randevus",
                newName: "IX_randevus_MusteriID");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_hizmetID",
                table: "randevus",
                newName: "IX_randevus_hizmetID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hizmets",
                table: "hizmets",
                column: "hizmetID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_uzmanlıks",
                table: "uzmanlıks",
                column: "UzmanlikID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_randevus",
                table: "randevus",
                column: "randevuID");

            migrationBuilder.AddForeignKey(
                name: "FK_hizmets_salons_salonID",
                table: "hizmets",
                column: "salonID",
                principalTable: "salons",
                principalColumn: "salonID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personnels_uzmanlıks_UzmanlikID",
                table: "personnels",
                column: "UzmanlikID",
                principalTable: "uzmanlıks",
                principalColumn: "UzmanlikID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_randevus_AspNetUsers_MusteriID",
                table: "randevus",
                column: "MusteriID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_randevus_hizmets_hizmetID",
                table: "randevus",
                column: "hizmetID",
                principalTable: "hizmets",
                principalColumn: "hizmetID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_randevus_personnels_personelID",
                table: "randevus",
                column: "personelID",
                principalTable: "personnels",
                principalColumn: "personelID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hizmets_salons_salonID",
                table: "hizmets");

            migrationBuilder.DropForeignKey(
                name: "FK_personnels_uzmanlıks_UzmanlikID",
                table: "personnels");

            migrationBuilder.DropForeignKey(
                name: "FK_randevus_AspNetUsers_MusteriID",
                table: "randevus");

            migrationBuilder.DropForeignKey(
                name: "FK_randevus_hizmets_hizmetID",
                table: "randevus");

            migrationBuilder.DropForeignKey(
                name: "FK_randevus_personnels_personelID",
                table: "randevus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_uzmanlıks",
                table: "uzmanlıks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_randevus",
                table: "randevus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hizmets",
                table: "hizmets");

            migrationBuilder.RenameTable(
                name: "uzmanlıks",
                newName: "expertises");

            migrationBuilder.RenameTable(
                name: "randevus",
                newName: "appointments");

            migrationBuilder.RenameTable(
                name: "hizmets",
                newName: "services");

            migrationBuilder.RenameIndex(
                name: "IX_randevus_personelID",
                table: "appointments",
                newName: "IX_appointments_personelID");

            migrationBuilder.RenameIndex(
                name: "IX_randevus_MusteriID",
                table: "appointments",
                newName: "IX_appointments_MusteriID");

            migrationBuilder.RenameIndex(
                name: "IX_randevus_hizmetID",
                table: "appointments",
                newName: "IX_appointments_hizmetID");

            migrationBuilder.RenameIndex(
                name: "IX_hizmets_salonID",
                table: "services",
                newName: "IX_services_salonID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_expertises",
                table: "expertises",
                column: "UzmanlikID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appointments",
                table: "appointments",
                column: "randevuID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_services",
                table: "services",
                column: "hizmetID");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_AspNetUsers_MusteriID",
                table: "appointments",
                column: "MusteriID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_personnels_personelID",
                table: "appointments",
                column: "personelID",
                principalTable: "personnels",
                principalColumn: "personelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_services_hizmetID",
                table: "appointments",
                column: "hizmetID",
                principalTable: "services",
                principalColumn: "hizmetID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personnels_expertises_UzmanlikID",
                table: "personnels",
                column: "UzmanlikID",
                principalTable: "expertises",
                principalColumn: "UzmanlikID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_services_salons_salonID",
                table: "services",
                column: "salonID",
                principalTable: "salons",
                principalColumn: "salonID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
