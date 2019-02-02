using Microsoft.EntityFrameworkCore.Migrations;

namespace DGT.Data.Migrations
{
    public partial class AddedMainRegularDriverIdToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainRegularDriverId",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainRegularDriverId",
                table: "Vehicles");
        }
    }
}
