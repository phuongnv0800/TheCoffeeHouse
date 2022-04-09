using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCH.BackendApi.Migrations
{
    public partial class updateb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMaterials_Materials_StockID",
                table: "StockMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMaterials_Stocks_StockID",
                table: "StockMaterials");

            migrationBuilder.DropTable(
                name: "ExportMaterials");

            migrationBuilder.DropTable(
                name: "ImportMaterials");

            migrationBuilder.DropTable(
                name: "LiquidationMaterials");

            migrationBuilder.DropTable(
                name: "RecipeMaterials");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "ExportReports");

            migrationBuilder.DropTable(
                name: "ImportReports");

            migrationBuilder.DropTable(
                name: "LiquidationReports");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "StockID",
                table: "StockMaterials",
                newName: "BranchID");

            migrationBuilder.AlterColumn<double>(
                name: "SubPrice",
                table: "Sizes",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Branchs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateTable(
                name: "RecipeDetails",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SizeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDetails", x => new { x.MaterialID, x.ProductID, x.SizeID });
                    table.ForeignKey(
                        name: "FK_RecipeDetails_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeDetails_Sizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Depreciation = table.Column<double>(type: "float", nullable: false),
                    LiquidationCost = table.Column<double>(type: "float", nullable: false),
                    Conclude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecoveryValue = table.Column<double>(type: "float", nullable: false),
                    LiquidationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LiquidationRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserCreateID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Report_Branchs_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branchs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportDetails",
                columns: table => new
                {
                    ReportID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PriceOfUnit = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDetails", x => new { x.ReportID, x.MaterialID });
                    table.ForeignKey(
                        name: "FK_ReportDetails_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportDetails_Report_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Report",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockMaterials_MaterialID",
                table: "StockMaterials",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_ProductID",
                table: "RecipeDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_SizeID",
                table: "RecipeDetails",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BranchID",
                table: "Report",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetails_MaterialID",
                table: "ReportDetails",
                column: "MaterialID");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMaterials_Branchs_BranchID",
                table: "StockMaterials",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMaterials_Materials_MaterialID",
                table: "StockMaterials",
                column: "MaterialID",
                principalTable: "Materials",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMaterials_Branchs_BranchID",
                table: "StockMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMaterials_Materials_MaterialID",
                table: "StockMaterials");

            migrationBuilder.DropTable(
                name: "RecipeDetails");

            migrationBuilder.DropTable(
                name: "ReportDetails");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropIndex(
                name: "IX_StockMaterials_MaterialID",
                table: "StockMaterials");

            migrationBuilder.RenameColumn(
                name: "BranchID",
                table: "StockMaterials",
                newName: "StockID");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubPrice",
                table: "Sizes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Branchs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ExportReports",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReports", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ImportReports",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    UserCreateID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportReports", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LiquidationReports",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Conclude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Depreciation = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LiquidationCost = table.Column<double>(type: "float", nullable: false),
                    LiquidationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LiquidationRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecoveryValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidationReports", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SizeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToppingID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ToppingName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Sizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Toppings_ToppingID",
                        column: x => x.ToppingID,
                        principalTable: "Toppings",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stocks_Branchs_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branchs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportMaterials",
                columns: table => new
                {
                    ExportID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expriydate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceOfUnit = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportMaterials", x => new { x.ExportID, x.MaterialID });
                    table.ForeignKey(
                        name: "FK_ExportMaterials_ExportReports_ExportID",
                        column: x => x.ExportID,
                        principalTable: "ExportReports",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportMaterials_Materials_ExportID",
                        column: x => x.ExportID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportMaterials",
                columns: table => new
                {
                    ImportID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expriydate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceOfUnit = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportMaterials", x => new { x.ImportID, x.MaterialID });
                    table.ForeignKey(
                        name: "FK_ImportMaterials_ImportReports_ImportID",
                        column: x => x.ImportID,
                        principalTable: "ImportReports",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportMaterials_Materials_ImportID",
                        column: x => x.ImportID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiquidationMaterials",
                columns: table => new
                {
                    LiquidationID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expriydate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceOfUnit = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidationMaterials", x => new { x.LiquidationID, x.MaterialID });
                    table.ForeignKey(
                        name: "FK_LiquidationMaterials_LiquidationReports_LiquidationID",
                        column: x => x.LiquidationID,
                        principalTable: "LiquidationReports",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiquidationMaterials_Materials_LiquidationID",
                        column: x => x.LiquidationID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductDetailID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recipes_ProductDetails_ProductDetailID",
                        column: x => x.ProductDetailID,
                        principalTable: "ProductDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeMaterials",
                columns: table => new
                {
                    MaterialID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMaterials", x => new { x.MaterialID, x.RecipeID });
                    table.ForeignKey(
                        name: "FK_RecipeMaterials_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeMaterials_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductID",
                table: "ProductDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_SizeID",
                table: "ProductDetails",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ToppingID",
                table: "ProductDetails",
                column: "ToppingID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMaterials_RecipeID",
                table: "RecipeMaterials",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProductDetailID",
                table: "Recipes",
                column: "ProductDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BranchID",
                table: "Stocks",
                column: "BranchID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMaterials_Materials_StockID",
                table: "StockMaterials",
                column: "StockID",
                principalTable: "Materials",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMaterials_Stocks_StockID",
                table: "StockMaterials",
                column: "StockID",
                principalTable: "Stocks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
