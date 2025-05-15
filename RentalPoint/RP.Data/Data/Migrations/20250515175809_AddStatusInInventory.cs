using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusInInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "Inventories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_StatusId",
                table: "Inventories",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_DictionaryValues_StatusId",
                table: "Inventories",
                column: "StatusId",
                principalTable: "DictionaryValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_DictionaryValues_StatusId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_StatusId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Inventories");
        }
    }
}
