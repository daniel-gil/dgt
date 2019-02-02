using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DGT.Data.Migrations
{
    public partial class AddedNumVehiclesToDriver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Infractions");

            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InfractionDate",
                table: "VehicleInfractions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<short>(
                name: "NumVehicles",
                table: "Drivers",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "VehicleDrivers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    VehicleId = table.Column<string>(nullable: true),
                    DriverId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDrivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleDrivers_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleDrivers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDrivers_DriverId",
                table: "VehicleDrivers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDrivers_VehicleId",
                table: "VehicleDrivers",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleDrivers");

            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "InfractionDate",
                table: "VehicleInfractions");

            migrationBuilder.DropColumn(
                name: "NumVehicles",
                table: "Drivers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Infractions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
