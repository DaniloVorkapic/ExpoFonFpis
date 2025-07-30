using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class insertNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Pregnancies_PregnancyId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Pregnancies_Employee_FemaleEmployeeId",
                table: "Pregnancies");

            migrationBuilder.CreateTable(
                name: "Manifestations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManifestationRegistrations_Capacity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifestations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exibitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManifestationId = table.Column<long>(type: "bigint", nullable: true),
                    ExibitionType_Value = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exibitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exibitions_Manifestations_ManifestationId",
                        column: x => x.ManifestationId,
                        principalTable: "Manifestations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManifestationRegistrations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManifestationId = table.Column<long>(type: "bigint", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_StreetOne = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_StreetTwo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_PostCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Address_CityName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmailAddres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManifestationRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManifestationRegistrations_Manifestations_ManifestationId",
                        column: x => x.ManifestationId,
                        principalTable: "Manifestations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exibitions_ManifestationId",
                table: "Exibitions",
                column: "ManifestationId");

            migrationBuilder.CreateIndex(
                name: "IX_ManifestationRegistrations_ManifestationId",
                table: "ManifestationRegistrations",
                column: "ManifestationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Pregnancies_PregnancyId",
                table: "Leaves",
                column: "PregnancyId",
                principalTable: "Pregnancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pregnancies_Employee_FemaleEmployeeId",
                table: "Pregnancies",
                column: "FemaleEmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Pregnancies_PregnancyId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Pregnancies_Employee_FemaleEmployeeId",
                table: "Pregnancies");

            migrationBuilder.DropTable(
                name: "Exibitions");

            migrationBuilder.DropTable(
                name: "ManifestationRegistrations");

            migrationBuilder.DropTable(
                name: "Manifestations");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Pregnancies_PregnancyId",
                table: "Leaves",
                column: "PregnancyId",
                principalTable: "Pregnancies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregnancies_Employee_FemaleEmployeeId",
                table: "Pregnancies",
                column: "FemaleEmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");
        }
    }
}
