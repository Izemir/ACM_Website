using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class initial34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Files",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Files");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Files",
                type: "bytea",
                nullable: true);
        }
    }
}
