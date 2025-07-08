using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipOtomasyonuApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    PersonelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TelefonNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maas = table.Column<double>(type: "float", nullable: false),
                    aktifMi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    yillikIzinHakki = table.Column<int>(type: "int", nullable: false),
                    IseBaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TCKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departman = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pozisyon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.PersonelID);
                });

            migrationBuilder.InsertData(
                table: "Personeller",
                columns: new[] { "PersonelID", "Ad", "Departman", "DogumTarihi", "Eposta", "IseBaslamaTarihi", "Maas", "Pozisyon", "Sifre", "Soyad", "TCKimlikNo", "TelefonNo", "aktifMi", "yillikIzinHakki" },
                values: new object[,]
                {
                    { 1, "Admin", "IT", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@company.com", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15000.0, "Yonetici", "123456", "User", "12345678901", "5551234567", "Aktif", 20 },
                    { 2, "Test", "Satış", new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "test@company.com", new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000.0, "Personel", "123456", "Personel", "11111111111", "5559876543", "Aktif", 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personeller");
        }
    }
}
