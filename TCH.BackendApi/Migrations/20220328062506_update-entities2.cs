using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCH.BackendApi.Migrations
{
    public partial class updateentities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_RoleGroup_RoleGroupID",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "RoleGroupID",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleGroupID",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleGroupID",
                table: "AspNetUsers",
                column: "RoleGroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_RoleGroup_RoleGroupID",
                table: "AspNetRoles",
                column: "RoleGroupID",
                principalTable: "RoleGroup",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RoleGroup_RoleGroupID",
                table: "AspNetUsers",
                column: "RoleGroupID",
                principalTable: "RoleGroup",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_RoleGroup_RoleGroupID",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RoleGroup_RoleGroupID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleGroupID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleGroupID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "RoleGroupID",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_RoleGroup_RoleGroupID",
                table: "AspNetRoles",
                column: "RoleGroupID",
                principalTable: "RoleGroup",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
