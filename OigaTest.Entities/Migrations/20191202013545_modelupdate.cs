using Microsoft.EntityFrameworkCore.Migrations;

namespace OigaTest.Entities.Migrations
{
    public partial class modelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_TenantId",
                table: "AppUsers",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Tenants_TenantId",
                table: "AppUsers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Tenants_TenantId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_TenantId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppUsers");
        }
    }
}
