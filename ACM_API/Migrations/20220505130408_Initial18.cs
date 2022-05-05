using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ACM_API.Migrations
{
    public partial class Initial18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ConstructionId",
                table: "Service",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Construction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConstructionName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Construction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Construction_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_ConstructionId",
                table: "Service",
                column: "ConstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_Construction_CustomerId",
                table: "Construction",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Construction_ConstructionId",
                table: "Service",
                column: "ConstructionId",
                principalTable: "Construction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Construction_ConstructionId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "Construction");

            migrationBuilder.DropIndex(
                name: "IX_Service_ConstructionId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ConstructionId",
                table: "Service");
        }
    }
}
