using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_AspNetUsers_CreatedById",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_AspNetUsers_LastChangedById",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Buildings_BuildingId1",
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
                name: "IX_MaintenanceRequests_BuildingId1",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_CreatedById",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_LastChangedById",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "BuildingId1",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "LastChangedById",
                table: "MaintenanceRequests");

            migrationBuilder.AddColumn<string>(
                name: "UnitNumber",
                table: "MaintenanceRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitNumber",
                table: "MaintenanceRequests");

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
                name: "BuildingId1",
                table: "MaintenanceRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "MaintenanceRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastChangedById",
                table: "MaintenanceRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_UserId",
                table: "Tenants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_BuildingId1",
                table: "MaintenanceRequests",
                column: "BuildingId1");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_CreatedById",
                table: "MaintenanceRequests",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_LastChangedById",
                table: "MaintenanceRequests",
                column: "LastChangedById");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_AspNetUsers_CreatedById",
                table: "MaintenanceRequests",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_AspNetUsers_LastChangedById",
                table: "MaintenanceRequests",
                column: "LastChangedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Buildings_BuildingId1",
                table: "MaintenanceRequests",
                column: "BuildingId1",
                principalTable: "Buildings",
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
    }
}
