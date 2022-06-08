using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class initial33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Files",
                newName: "FileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Files",
                type: "bytea",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Files",
                newName: "Data");
        }
    }
}
