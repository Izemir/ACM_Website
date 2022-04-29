using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ACM_API.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Customers_CustomerId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ActualAddress",
                table: "Executors");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Executors");

            migrationBuilder.DropColumn(
                name: "KPP",
                table: "Executors");

            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Executors");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "OGRN",
                table: "Executors",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Contact",
                newName: "ContactTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_CustomerId",
                table: "Contact",
                newName: "IX_Contact_ContactTypeId");

            migrationBuilder.AddColumn<long>(
                name: "SpecialityId",
                table: "Executors",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerTypeId",
                table: "Customers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ContactPersonId",
                table: "Contact",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactPerson",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPerson_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpecialityName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Executors_SpecialityId",
                table: "Executors",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactPersonId",
                table: "Contact",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPerson_CustomerId",
                table: "ContactPerson",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_ContactPerson_ContactPersonId",
                table: "Contact",
                column: "ContactPersonId",
                principalTable: "ContactPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_ContactType_ContactTypeId",
                table: "Contact",
                column: "ContactTypeId",
                principalTable: "ContactType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerType_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId",
                principalTable: "CustomerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Executors_Speciality_SpecialityId",
                table: "Executors",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_ContactPerson_ContactPersonId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_ContactType_ContactTypeId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerType_CustomerTypeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Executors_Speciality_SpecialityId",
                table: "Executors");

            migrationBuilder.DropTable(
                name: "ContactPerson");

            migrationBuilder.DropTable(
                name: "ContactType");

            migrationBuilder.DropTable(
                name: "CustomerType");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Executors_SpecialityId",
                table: "Executors");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ContactPersonId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Executors");

            migrationBuilder.DropColumn(
                name: "CustomerTypeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ContactPersonId",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Executors",
                newName: "OGRN");

            migrationBuilder.RenameColumn(
                name: "ContactTypeId",
                table: "Contact",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_ContactTypeId",
                table: "Contact",
                newName: "IX_Contact_CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "ActualAddress",
                table: "Executors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Executors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KPP",
                table: "Executors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Speciality",
                table: "Executors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Contact",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Customers_CustomerId",
                table: "Contact",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
