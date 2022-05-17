using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class Initial28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Chats",
                newName: "ExecutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                newName: "IX_Chats_ExecutorId");

            migrationBuilder.AddColumn<string>(
                name: "ChatName",
                table: "Chats",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Chats",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_CustomerId",
                table: "Chats",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Customers_CustomerId",
                table: "Chats",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Executors_ExecutorId",
                table: "Chats",
                column: "ExecutorId",
                principalTable: "Executors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Customers_CustomerId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Executors_ExecutorId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_CustomerId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatName",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "ExecutorId",
                table: "Chats",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_ExecutorId",
                table: "Chats",
                newName: "IX_Chats_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
