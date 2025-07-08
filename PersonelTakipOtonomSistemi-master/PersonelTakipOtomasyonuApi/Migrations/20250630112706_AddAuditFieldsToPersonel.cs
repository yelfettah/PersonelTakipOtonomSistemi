using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipOtomasyonuApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFieldsToPersonel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EklenmeTarihi",
                table: "Personeller",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EkleyenKullanici",
                table: "Personeller",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellenmeTarihi",
                table: "Personeller",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuncelleyenKullanici",
                table: "Personeller",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Personeller",
                keyColumn: "PersonelID",
                keyValue: 1,
                columns: new[] { "EklenmeTarihi", "EkleyenKullanici", "GuncellenmeTarihi", "GuncelleyenKullanici" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Personeller",
                keyColumn: "PersonelID",
                keyValue: 2,
                columns: new[] { "EklenmeTarihi", "EkleyenKullanici", "GuncellenmeTarihi", "GuncelleyenKullanici" },
                values: new object[] { null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EklenmeTarihi",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "EkleyenKullanici",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "GuncellenmeTarihi",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "GuncelleyenKullanici",
                table: "Personeller");
        }
    }
}
