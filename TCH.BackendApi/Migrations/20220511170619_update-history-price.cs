using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCH.BackendApi.Migrations
{
    public partial class updatehistoryprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCreateID",
                table: "Toppings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdateID",
                table: "Toppings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreateID",
                table: "Sizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdateID",
                table: "Sizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoryPriceUpdates",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HistoryType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceOld = table.Column<double>(type: "float", nullable: false),
                    PriceNew = table.Column<double>(type: "float", nullable: false),
                    UserCreateID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SizeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ToppingID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryPriceUpdates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HistoryPriceUpdates_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HistoryPriceUpdates_Sizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HistoryPriceUpdates_Toppings_ToppingID",
                        column: x => x.ToppingID,
                        principalTable: "Toppings",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPriceUpdates_ProductID",
                table: "HistoryPriceUpdates",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPriceUpdates_SizeID",
                table: "HistoryPriceUpdates",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPriceUpdates_ToppingID",
                table: "HistoryPriceUpdates",
                column: "ToppingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryPriceUpdates");

            migrationBuilder.DropColumn(
                name: "UserCreateID",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "UserUpdateID",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "UserCreateID",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "UserUpdateID",
                table: "Sizes");
        }
    }
}
