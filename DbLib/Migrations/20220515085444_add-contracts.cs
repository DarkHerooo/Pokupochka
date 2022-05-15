using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbLib.Migrations
{
    public partial class addcontracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Counterparties_ClientId",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Contract",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ClientId",
                table: "Contract",
                newName: "IX_Contract_StatusId");

            migrationBuilder.AddColumn<int>(
                name: "CountYears",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CounterpartyId",
                table: "Contract",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractProduct",
                columns: table => new
                {
                    ContractsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractProduct", x => new { x.ContractsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ContractProduct_Contract_ContractsId",
                        column: x => x.ContractsId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractProduct_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CounterpartyId",
                table: "Contract",
                column: "CounterpartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractProduct_ProductsId",
                table: "ContractProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Counterparties_CounterpartyId",
                table: "Contract",
                column: "CounterpartyId",
                principalTable: "Counterparties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Status_StatusId",
                table: "Contract",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Counterparties_CounterpartyId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Status_StatusId",
                table: "Contract");

            migrationBuilder.DropTable(
                name: "ContractProduct");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Contract_CounterpartyId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "CountYears",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "CounterpartyId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Contract",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_StatusId",
                table: "Contract",
                newName: "IX_Contract_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Counterparties_ClientId",
                table: "Contract",
                column: "ClientId",
                principalTable: "Counterparties",
                principalColumn: "Id");
        }
    }
}
