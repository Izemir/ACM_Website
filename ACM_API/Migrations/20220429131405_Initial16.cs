using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class Initial16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetencyExecutor",
                table: "CompetencyExecutor");

            migrationBuilder.DropIndex(
                name: "IX_CompetencyExecutor_ExecutorId",
                table: "CompetencyExecutor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetencyExecutor",
                table: "CompetencyExecutor",
                columns: new[] { "ExecutorId", "CompetencyId" });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyExecutor_CompetencyId",
                table: "CompetencyExecutor",
                column: "CompetencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetencyExecutor",
                table: "CompetencyExecutor");

            migrationBuilder.DropIndex(
                name: "IX_CompetencyExecutor_CompetencyId",
                table: "CompetencyExecutor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetencyExecutor",
                table: "CompetencyExecutor",
                columns: new[] { "CompetencyId", "ExecutorId" });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyExecutor_ExecutorId",
                table: "CompetencyExecutor",
                column: "ExecutorId");
        }
    }
}
