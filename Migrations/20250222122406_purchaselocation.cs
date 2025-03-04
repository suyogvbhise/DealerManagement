using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DMS.Migrations
{
    /// <inheritdoc />
    public partial class purchaselocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseLocationId",
                table: "Inventory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PurchaseLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseLocations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PurchaseLocations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Showroom A" },
                    { 2, "Dealer X" },
                    { 3, "Warehouse Y" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_PurchaseLocationId",
                table: "Inventory",
                column: "PurchaseLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_PurchaseLocations_PurchaseLocationId",
                table: "Inventory",
                column: "PurchaseLocationId",
                principalTable: "PurchaseLocations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_PurchaseLocations_PurchaseLocationId",
                table: "Inventory");

            migrationBuilder.DropTable(
                name: "PurchaseLocations");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_PurchaseLocationId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "PurchaseLocationId",
                table: "Inventory");
        }
    }
}
