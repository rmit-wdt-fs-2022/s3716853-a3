using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment03.Migrations
{
    public partial class Database_Model_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    HomeAddressId = table.Column<int>(type: "int", nullable: true),
                    WorkAddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contacts_Addresses_HomeAddressId",
                        column: x => x.HomeAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_Contacts_Addresses_WorkAddressId",
                        column: x => x.WorkAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_HomeAddressId",
                table: "Contacts",
                column: "HomeAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_WorkAddressId",
                table: "Contacts",
                column: "WorkAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
