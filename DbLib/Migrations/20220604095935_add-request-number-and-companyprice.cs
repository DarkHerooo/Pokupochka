using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbLib.Migrations
{
    public partial class addrequestnumberandcompanyprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "float(15)",
                precision: 15,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<double>(
                name: "CompanyPrice",
                table: "Products",
                type: "float(15)",
                precision: 15,
                scale: 2,
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CompanyPrice",
                table: "Products");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(15)",
                oldPrecision: 15,
                oldScale: 2);
        }
    }
}
