using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ACM_API.Migrations
{
    public partial class Initial38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubCustomerId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    ContactPersonId = table.Column<long>(type: "bigint", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    OGRN = table.Column<string>(type: "text", nullable: true),
                    INN = table.Column<string>(type: "text", nullable: true),
                    KPP = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    ActualAddress = table.Column<string>(type: "text", nullable: true),
                    CustomerTypeId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCustomers_ContactPerson_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "ContactPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubCustomers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubCustomers_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatSubCustomer",
                columns: table => new
                {
                    ChatsId = table.Column<long>(type: "bigint", nullable: false),
                    SubCustomersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSubCustomer", x => new { x.ChatsId, x.SubCustomersId });
                    table.ForeignKey(
                        name: "FK_ChatSubCustomer_Chats_ChatsId",
                        column: x => x.ChatsId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatSubCustomer_SubCustomers_SubCustomersId",
                        column: x => x.SubCustomersId,
                        principalTable: "SubCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubCustomerId",
                table: "Users",
                column: "SubCustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatSubCustomer_SubCustomersId",
                table: "ChatSubCustomer",
                column: "SubCustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCustomers_ContactPersonId",
                table: "SubCustomers",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCustomers_CustomerId",
                table: "SubCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCustomers_CustomerTypeId",
                table: "SubCustomers",
                column: "CustomerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SubCustomers_SubCustomerId",
                table: "Users",
                column: "SubCustomerId",
                principalTable: "SubCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SubCustomers_SubCustomerId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ChatSubCustomer");

            migrationBuilder.DropTable(
                name: "SubCustomers");

            migrationBuilder.DropIndex(
                name: "IX_Users_SubCustomerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SubCustomerId",
                table: "Users");
        }
    }
}
