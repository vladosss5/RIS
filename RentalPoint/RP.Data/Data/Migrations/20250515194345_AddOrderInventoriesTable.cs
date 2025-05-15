using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderInventoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FullPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderInventories",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "TEXT", nullable: false),
                    InventoryId = table.Column<string>(type: "TEXT", nullable: false),
                    ReturnDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInventories", x => new { x.OrderId, x.InventoryId });
                    table.ForeignKey(
                        name: "FK_OrderInventories_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderInventories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dictionaries_Type",
                table: "Dictionaries",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderInventories_InventoryId",
                table: "OrderInventories",
                column: "InventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderInventories");

            migrationBuilder.DropIndex(
                name: "IX_Dictionaries_Type",
                table: "Dictionaries");

            migrationBuilder.AlterColumn<decimal>(
                name: "FullPrice",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }
    }
}
