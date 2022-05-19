using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbLib.Migrations
{
    public partial class aaaaaaaaaaaaaaaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Counterparties_CounterpartyId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Status_StatusId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Workers_WorkerId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractProduct_Contract_ContractsId",
                table: "ContractProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractProduct_Product_ProductsId",
                table: "ContractProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRequest_Product_ProductsId",
                table: "ProductRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRequest_Request_RequestsId",
                table: "ProductRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Counterparties_CounterpartyId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Status_StatusId",
                table: "Request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "Requests");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_Request_StatusId",
                table: "Requests",
                newName: "IX_Requests_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_CounterpartyId",
                table: "Requests",
                newName: "IX_Requests_CounterpartyId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_WorkerId",
                table: "Contracts",
                newName: "IX_Contracts_WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_StatusId",
                table: "Contracts",
                newName: "IX_Contracts_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_CounterpartyId",
                table: "Contracts",
                newName: "IX_Contracts_CounterpartyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductWarehouse",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    WarehousesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWarehouse", x => new { x.ProductsId, x.WarehousesId });
                    table.ForeignKey(
                        name: "FK_ProductWarehouse_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWarehouse_Warehouses_WarehousesId",
                        column: x => x.WarehousesId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_WarehousesId",
                table: "ProductWarehouse",
                column: "WarehousesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractProduct_Contracts_ContractsId",
                table: "ContractProduct",
                column: "ContractsId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractProduct_Products_ProductsId",
                table: "ContractProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Counterparties_CounterpartyId",
                table: "Contracts",
                column: "CounterpartyId",
                principalTable: "Counterparties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Status_StatusId",
                table: "Contracts",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Workers_WorkerId",
                table: "Contracts",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRequest_Products_ProductsId",
                table: "ProductRequest",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRequest_Requests_RequestsId",
                table: "ProductRequest",
                column: "RequestsId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Counterparties_CounterpartyId",
                table: "Requests",
                column: "CounterpartyId",
                principalTable: "Counterparties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Status_StatusId",
                table: "Requests",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractProduct_Contracts_ContractsId",
                table: "ContractProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractProduct_Products_ProductsId",
                table: "ContractProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Counterparties_CounterpartyId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Status_StatusId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Workers_WorkerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRequest_Products_ProductsId",
                table: "ProductRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRequest_Requests_RequestsId",
                table: "ProductRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Counterparties_CounterpartyId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Status_StatusId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "ProductWarehouse");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Request");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_StatusId",
                table: "Request",
                newName: "IX_Request_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_CounterpartyId",
                table: "Request",
                newName: "IX_Request_CounterpartyId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_WorkerId",
                table: "Contract",
                newName: "IX_Contract_WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_StatusId",
                table: "Contract",
                newName: "IX_Contract_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CounterpartyId",
                table: "Contract",
                newName: "IX_Contract_CounterpartyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Workers_WorkerId",
                table: "Contract",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractProduct_Contract_ContractsId",
                table: "ContractProduct",
                column: "ContractsId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractProduct_Product_ProductsId",
                table: "ContractProduct",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRequest_Product_ProductsId",
                table: "ProductRequest",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRequest_Request_RequestsId",
                table: "ProductRequest",
                column: "RequestsId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Counterparties_CounterpartyId",
                table: "Request",
                column: "CounterpartyId",
                principalTable: "Counterparties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Status_StatusId",
                table: "Request",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
