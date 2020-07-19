using Microsoft.EntityFrameworkCore.Migrations;

namespace Reply.Exam.WebAPI.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Name = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInsurance",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CustomerID = table.Column<int>(nullable: false),
                    VehicleID = table.Column<int>(nullable: false),
                    InsuredValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInsurance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerInsurance_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerInsurance_Vehicle_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInsurance_CustomerID",
                table: "CustomerInsurance",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInsurance_VehicleID",
                table: "CustomerInsurance",
                column: "VehicleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerInsurance");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
