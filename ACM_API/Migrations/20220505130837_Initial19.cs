using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class Initial19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Construction_ConstructionId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_ConstructionId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ConstructionId",
                table: "Service");

            migrationBuilder.CreateTable(
                name: "ConstructionService",
                columns: table => new
                {
                    ConstructionsId = table.Column<long>(type: "bigint", nullable: false),
                    ServicesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionService", x => new { x.ConstructionsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ConstructionService_Construction_ConstructionsId",
                        column: x => x.ConstructionsId,
                        principalTable: "Construction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructionService_Service_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionService_ServicesId",
                table: "ConstructionService",
                column: "ServicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructionService");

            migrationBuilder.AddColumn<long>(
                name: "ConstructionId",
                table: "Service",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_ConstructionId",
                table: "Service",
                column: "ConstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Construction_ConstructionId",
                table: "Service",
                column: "ConstructionId",
                principalTable: "Construction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
