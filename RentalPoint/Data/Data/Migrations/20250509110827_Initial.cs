using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FName = table.Column<string>(type: "TEXT", nullable: false),
                    SName = table.Column<string>(type: "TEXT", nullable: false),
                    LName = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dictionaries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryValues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DictionaryId = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictionaryValues_Dictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "Dictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DocumentNumber = table.Column<string>(type: "TEXT", nullable: false),
                    TypeId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposits_DictionaryValues_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DictionaryValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_DictionaryValues_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DictionaryValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FullPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    ClientId = table.Column<string>(type: "TEXT", nullable: true),
                    StatusId = table.Column<string>(type: "TEXT", nullable: false),
                    DepositId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Deposits_DepositId",
                        column: x => x.DepositId,
                        principalTable: "Deposits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_DictionaryValues_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DictionaryValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_TypeId",
                table: "Deposits",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryValues_DictionaryId",
                table: "DictionaryValues",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_TypeId",
                table: "Inventories",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DepositId",
                table: "Orders",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "DictionaryValues");

            migrationBuilder.DropTable(
                name: "Dictionaries");
        }
    }
}
