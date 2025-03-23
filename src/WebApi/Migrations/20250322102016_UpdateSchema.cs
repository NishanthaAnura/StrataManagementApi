using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "MaintenanceRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "MaintenanceRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_UserId",
                table: "Tenants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_OwnerId",
                table: "MaintenanceRequests",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_TenantId",
                table: "MaintenanceRequests",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Owners_OwnerId",
                table: "MaintenanceRequests",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Tenants_TenantId",
                table: "MaintenanceRequests",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_AspNetUsers_UserId",
                table: "Owners",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_AspNetUsers_UserId",
                table: "Tenants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Owners_OwnerId",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Tenants_TenantId",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_AspNetUsers_UserId",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_AspNetUsers_UserId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_UserId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Owners_UserId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_OwnerId",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_TenantId",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "MaintenanceRequests");
        }
    }
}
