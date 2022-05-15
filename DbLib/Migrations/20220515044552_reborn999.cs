using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbLib.Migrations
{
    public partial class reborn999 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counterparties_DbImages_DbImageId",
                table: "Counterparties");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_DbImages_DbImageId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "DbImages");

            migrationBuilder.DropIndex(
                name: "IX_Roles_DbImageId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Counterparties_DbImageId",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "DbImageId",
                table: "Counterparties");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Counterparties",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Counterparties");

            migrationBuilder.AddColumn<int>(
                name: "DbImageId",
                table: "Counterparties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DbImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbImages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DbImageId",
                table: "Roles",
                column: "DbImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Counterparties_DbImageId",
                table: "Counterparties",
                column: "DbImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Counterparties_DbImages_DbImageId",
                table: "Counterparties",
                column: "DbImageId",
                principalTable: "DbImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_DbImages_DbImageId",
                table: "Roles",
                column: "DbImageId",
                principalTable: "DbImages",
                principalColumn: "Id");
        }
    }
}
