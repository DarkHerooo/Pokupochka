using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbLib.Migrations
{
    public partial class lastmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Counterparties");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Counterparties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    INN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KPP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OKPO = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counterparties_CompanyId",
                table: "Counterparties",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Counterparties_Companies_CompanyId",
                table: "Counterparties",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counterparties_Companies_CompanyId",
                table: "Counterparties");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Counterparties_CompanyId",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Counterparties");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Counterparties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Counterparties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Counterparties",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
