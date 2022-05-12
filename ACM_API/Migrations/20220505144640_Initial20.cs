using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class Initial20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyService_Service_ServiceId",
                table: "CompetencyService");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionService_Service_ServicesId",
                table: "ConstructionService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Services");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyService_Services_ServiceId",
                table: "CompetencyService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionService_Services_ServicesId",
                table: "ConstructionService",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyService_Services_ServiceId",
                table: "CompetencyService");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionService_Services_ServicesId",
                table: "ConstructionService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Service");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyService_Service_ServiceId",
                table: "CompetencyService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionService_Service_ServicesId",
                table: "ConstructionService",
                column: "ServicesId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
