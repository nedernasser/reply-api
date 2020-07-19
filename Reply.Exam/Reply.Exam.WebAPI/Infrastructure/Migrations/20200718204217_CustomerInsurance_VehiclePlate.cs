using Microsoft.EntityFrameworkCore.Migrations;

namespace Reply.Exam.WebAPI.Infrastructure.Migrations
{
    public partial class CustomerInsurance_VehiclePlate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehiclePlate",
                table: "CustomerInsurance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehiclePlate",
                table: "CustomerInsurance");
        }
    }
}
