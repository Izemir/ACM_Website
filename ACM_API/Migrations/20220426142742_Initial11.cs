using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class Initial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyExecutor_Competency_CompetencyId",
                table: "CompetencyExecutor");

            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyService_Competency_CompetencyId",
                table: "CompetencyService");

            migrationBuilder.DropForeignKey(
                name: "FK_Executors_Speciality_SpecialityId",
                table: "Executors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competency",
                table: "Competency");

            migrationBuilder.RenameTable(
                name: "Speciality",
                newName: "Specialities");

            migrationBuilder.RenameTable(
                name: "Competency",
                newName: "Competencies");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialities",
                table: "Specialities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competencies",
                table: "Competencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyExecutor_Competencies_CompetencyId",
                table: "CompetencyExecutor",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyService_Competencies_CompetencyId",
                table: "CompetencyService",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Executors_Specialities_SpecialityId",
                table: "Executors",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyExecutor_Competencies_CompetencyId",
                table: "CompetencyExecutor");

            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyService_Competencies_CompetencyId",
                table: "CompetencyService");

            migrationBuilder.DropForeignKey(
                name: "FK_Executors_Specialities_SpecialityId",
                table: "Executors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialities",
                table: "Specialities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competencies",
                table: "Competencies");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Specialities",
                newName: "Speciality");

            migrationBuilder.RenameTable(
                name: "Competencies",
                newName: "Competency");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competency",
                table: "Competency",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyExecutor_Competency_CompetencyId",
                table: "CompetencyExecutor",
                column: "CompetencyId",
                principalTable: "Competency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyService_Competency_CompetencyId",
                table: "CompetencyService",
                column: "CompetencyId",
                principalTable: "Competency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Executors_Speciality_SpecialityId",
                table: "Executors",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
