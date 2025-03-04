using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Migrations
{
    /// <inheritdoc />
    public partial class AddFuelTypeTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FuelTypeId",
                table: "Inventory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_FuelTypeId",
                table: "Inventory",
                column: "FuelTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_FuelTypes_FuelTypeId",
                table: "Inventory",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_FuelTypes_FuelTypeId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_FuelTypeId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "FuelTypeId",
                table: "Inventory");
        }
    }
}
