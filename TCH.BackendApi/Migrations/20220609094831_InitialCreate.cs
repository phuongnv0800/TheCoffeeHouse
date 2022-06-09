using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCH.BackendApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UnitConversions",
                newName: "UnitConversionID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Toppings",
                newName: "ToppingID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Sizes",
                newName: "SizeID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Reports",
                newName: "ReportID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Promotions",
                newName: "PromotionID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Products",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ProductImages",
                newName: "ProductImageID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Orders",
                newName: "OrderID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Menus",
                newName: "MenuID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "MemberTypes",
                newName: "BeanID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Measures",
                newName: "MeasureID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "MaterialTypes",
                newName: "MaterialTypeID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Materials",
                newName: "MaterialID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "HistoryPriceUpdates",
                newName: "HistoryPriceUpdateID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Customers",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Branchs",
                newName: "BranchID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitConversionID",
                table: "UnitConversions",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ToppingID",
                table: "Toppings",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SizeID",
                table: "Sizes",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ReportID",
                table: "Reports",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PromotionID",
                table: "Promotions",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Products",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ProductImageID",
                table: "ProductImages",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Orders",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "MenuID",
                table: "Menus",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "BeanID",
                table: "MemberTypes",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "MeasureID",
                table: "Measures",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "MaterialTypeID",
                table: "MaterialTypes",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "MaterialID",
                table: "Materials",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "HistoryPriceUpdateID",
                table: "HistoryPriceUpdates",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Customers",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "BranchID",
                table: "Branchs",
                newName: "ID");
        }
    }
}
