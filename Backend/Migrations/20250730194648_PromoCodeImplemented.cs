using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class PromoCodeImplemented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPromoCode",
                table: "ManifestationRegistrations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PromoCodeUsed",
                table: "ManifestationRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManifestationRegistrationId = table.Column<long>(type: "bigint", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManifestationId = table.Column<long>(type: "bigint", nullable: true),
                    LifecycleStatus_Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromoCodes_ManifestationRegistrations_ManifestationRegistrationId",
                        column: x => x.ManifestationRegistrationId,
                        principalTable: "ManifestationRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PromoCodes_Manifestations_ManifestationId",
                        column: x => x.ManifestationId,
                        principalTable: "Manifestations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_ManifestationId",
                table: "PromoCodes",
                column: "ManifestationId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_ManifestationRegistrationId",
                table: "PromoCodes",
                column: "ManifestationRegistrationId",
                unique: true,
                filter: "[ManifestationRegistrationId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "HasPromoCode",
                table: "ManifestationRegistrations");

            migrationBuilder.DropColumn(
                name: "PromoCodeUsed",
                table: "ManifestationRegistrations");
        }
    }
}
