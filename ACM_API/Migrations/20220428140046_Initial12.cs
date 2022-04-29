using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class Initial12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Executors_Specialities_SpecialityId",
                table: "Executors");

            migrationBuilder.DropIndex(
                name: "IX_Executors_SpecialityId",
                table: "Executors");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Executors");

            migrationBuilder.CreateTable(
                name: "ExecutorSpeciality",
                columns: table => new
                {
                    ExecutorId = table.Column<long>(type: "bigint", nullable: false),
                    SpecialityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutorSpeciality", x => new { x.ExecutorId, x.SpecialityId });
                    table.ForeignKey(
                        name: "FK_ExecutorSpeciality_Executors_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "Executors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExecutorSpeciality_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutorSpeciality_SpecialityId",
                table: "ExecutorSpeciality",
                column: "SpecialityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutorSpeciality");

            migrationBuilder.AddColumn<long>(
                name: "SpecialityId",
                table: "Executors",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Executors_SpecialityId",
                table: "Executors",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Executors_Specialities_SpecialityId",
                table: "Executors",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
