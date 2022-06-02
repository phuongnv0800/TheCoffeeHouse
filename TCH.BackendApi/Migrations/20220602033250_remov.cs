using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCH.BackendApi.Migrations
{
    public partial class remov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceToppping1",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "PriceToppping2",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Topping1Name",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Topping2Name",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ToppingID1",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ToppingID2",
                table: "OrderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PriceToppping1",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceToppping2",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Topping1Name",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Topping2Name",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToppingID1",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToppingID2",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
