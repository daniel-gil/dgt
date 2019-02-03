using Microsoft.EntityFrameworkCore.Migrations;

namespace DGT.Data.Migrations
{
    public partial class AddedTopInfractionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                table: "VehicleInfractions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInfractions_DriverId",
                table: "VehicleInfractions",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInfractions_Drivers_DriverId",
                table: "VehicleInfractions",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DNI",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInfractions_Drivers_DriverId",
                table: "VehicleInfractions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleInfractions_DriverId",
                table: "VehicleInfractions");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "VehicleInfractions");
        }
    }
}
