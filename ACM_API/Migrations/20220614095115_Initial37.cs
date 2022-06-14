using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class Initial37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "Files",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_OrderId",
                table: "Files",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Orders_OrderId",
                table: "Files",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Orders_OrderId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_OrderId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Files");
        }
    }
}
