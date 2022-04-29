using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ACM_API.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competency",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetencyName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyExecutor",
                columns: table => new
                {
                    CompetenciesId = table.Column<long>(type: "bigint", nullable: false),
                    ExecutorsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyExecutor", x => new { x.CompetenciesId, x.ExecutorsId });
                    table.ForeignKey(
                        name: "FK_CompetencyExecutor_Competency_CompetenciesId",
                        column: x => x.CompetenciesId,
                        principalTable: "Competency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyExecutor_Executors_ExecutorsId",
                        column: x => x.ExecutorsId,
                        principalTable: "Executors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyExecutor_ExecutorsId",
                table: "CompetencyExecutor",
                column: "ExecutorsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetencyExecutor");

            migrationBuilder.DropTable(
                name: "Competency");
        }
    }
}
