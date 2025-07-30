using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOwsOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManifestationRegistrations_Manifestations_ManifestationId",
                table: "ManifestationRegistrations");

            migrationBuilder.RenameColumn(
                name: "ManifestationRegistrations_Capacity",
                table: "Manifestations",
                newName: "Capacity");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Manifestations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ManifestationRegistrations_Manifestations_ManifestationId",
                table: "ManifestationRegistrations",
                column: "ManifestationId",
                principalTable: "Manifestations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManifestationRegistrations_Manifestations_ManifestationId",
                table: "ManifestationRegistrations");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Manifestations",
                newName: "ManifestationRegistrations_Capacity");

            migrationBuilder.AlterColumn<int>(
                name: "ManifestationRegistrations_Capacity",
                table: "Manifestations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ManifestationRegistrations_Manifestations_ManifestationId",
                table: "ManifestationRegistrations",
                column: "ManifestationId",
                principalTable: "Manifestations",
                principalColumn: "Id");
        }
    }
}
