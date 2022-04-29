using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ACM_API.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyExecutor_Competency_CompetenciesId",
                table: "CompetencyExecutor");

            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyExecutor_Executors_ExecutorsId",
                table: "CompetencyExecutor");

            migrationBuilder.RenameColumn(
                name: "ExecutorsId",
                table: "CompetencyExecutor",
                newName: "ExecutorId");

            migrationBuilder.RenameColumn(
                name: "CompetenciesId",
                table: "CompetencyExecutor",
                newName: "CompetencyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompetencyExecutor_ExecutorsId",
                table: "CompetencyExecutor",
                newName: "IX_CompetencyExecutor_ExecutorId");

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IndustryName = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Industry_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyService",
                columns: table => new
                {
                    CompetencyId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyService", x => new { x.CompetencyId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_CompetencyService_Competency_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyService_ServiceId",
                table: "CompetencyService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Industry_CustomerId",
                table: "Industry",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyExecutor_Competency_CompetencyId",
                table: "CompetencyExecutor",
                column: "CompetencyId",
                principalTable: "Competency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyExecutor_Executors_ExecutorId",
                table: "CompetencyExecutor",
                column: "ExecutorId",
                principalTable: "Executors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyExecutor_Competency_CompetencyId",
                table: "CompetencyExecutor");

            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyExecutor_Executors_ExecutorId",
                table: "CompetencyExecutor");

            migrationBuilder.DropTable(
                name: "CompetencyService");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.RenameColumn(
                name: "ExecutorId",
                table: "CompetencyExecutor",
                newName: "ExecutorsId");

            migrationBuilder.RenameColumn(
                name: "CompetencyId",
                table: "CompetencyExecutor",
                newName: "CompetenciesId");

            migrationBuilder.RenameIndex(
                name: "IX_CompetencyExecutor_ExecutorId",
                table: "CompetencyExecutor",
                newName: "IX_CompetencyExecutor_ExecutorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyExecutor_Competency_CompetenciesId",
                table: "CompetencyExecutor",
                column: "CompetenciesId",
                principalTable: "Competency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyExecutor_Executors_ExecutorsId",
                table: "CompetencyExecutor",
                column: "ExecutorsId",
                principalTable: "Executors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
