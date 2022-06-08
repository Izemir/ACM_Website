using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class initial32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFile_Executors_ExecutorId",
                table: "UserFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFile",
                table: "UserFile");

            migrationBuilder.RenameTable(
                name: "UserFile",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_UserFile_ExecutorId",
                table: "Files",
                newName: "IX_Files_ExecutorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Executors_ExecutorId",
                table: "Files",
                column: "ExecutorId",
                principalTable: "Executors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Executors_ExecutorId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "UserFile");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ExecutorId",
                table: "UserFile",
                newName: "IX_UserFile_ExecutorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFile",
                table: "UserFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFile_Executors_ExecutorId",
                table: "UserFile",
                column: "ExecutorId",
                principalTable: "Executors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
