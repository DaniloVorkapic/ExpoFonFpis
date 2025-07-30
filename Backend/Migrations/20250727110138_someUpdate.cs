using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class someUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_StreetTwo",
                table: "ManifestationRegistrations",
                newName: "StreetTwo");

            migrationBuilder.RenameColumn(
                name: "Address_StreetOne",
                table: "ManifestationRegistrations",
                newName: "StreetOne");

            migrationBuilder.RenameColumn(
                name: "Address_PostCode",
                table: "ManifestationRegistrations",
                newName: "PostCode");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "ManifestationRegistrations",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Address_CityName",
                table: "ManifestationRegistrations",
                newName: "CityName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetTwo",
                table: "ManifestationRegistrations",
                newName: "Address_StreetTwo");

            migrationBuilder.RenameColumn(
                name: "StreetOne",
                table: "ManifestationRegistrations",
                newName: "Address_StreetOne");

            migrationBuilder.RenameColumn(
                name: "PostCode",
                table: "ManifestationRegistrations",
                newName: "Address_PostCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "ManifestationRegistrations",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "ManifestationRegistrations",
                newName: "Address_CityName");
        }
    }
}
