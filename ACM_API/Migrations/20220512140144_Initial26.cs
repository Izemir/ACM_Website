using Microsoft.EntityFrameworkCore.Migrations;

namespace ACM_API.Migrations
{
    public partial class Initial26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Construction_Customers_CustomerId",
                table: "Construction");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionService_Construction_ConstructionsId",
                table: "ConstructionService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Construction",
                table: "Construction");

            migrationBuilder.RenameTable(
                name: "Construction",
                newName: "Constructions");

            migrationBuilder.RenameIndex(
                name: "IX_Construction_CustomerId",
                table: "Constructions",
                newName: "IX_Constructions_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Constructions",
                table: "Constructions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Constructions_Customers_CustomerId",
                table: "Constructions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionService_Constructions_ConstructionsId",
                table: "ConstructionService",
                column: "ConstructionsId",
                principalTable: "Constructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Constructions_Customers_CustomerId",
                table: "Constructions");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionService_Constructions_ConstructionsId",
                table: "ConstructionService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Constructions",
                table: "Constructions");

            migrationBuilder.RenameTable(
                name: "Constructions",
                newName: "Construction");

            migrationBuilder.RenameIndex(
                name: "IX_Constructions_CustomerId",
                table: "Construction",
                newName: "IX_Construction_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Construction",
                table: "Construction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Construction_Customers_CustomerId",
                table: "Construction",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionService_Construction_ConstructionsId",
                table: "ConstructionService",
                column: "ConstructionsId",
                principalTable: "Construction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
