using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Variations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variations_ItemId",
                table: "Variations",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ProviderId",
                table: "Reservations",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OperatorId",
                table: "Orders",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_OperatorId",
                table: "Orders",
                column: "OperatorId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Employees_ProviderId",
                table: "Reservations",
                column: "ProviderId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Variations_Items_ItemId",
                table: "Variations",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_OperatorId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Employees_ProviderId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Variations_Items_ItemId",
                table: "Variations");

            migrationBuilder.DropIndex(
                name: "IX_Variations_ItemId",
                table: "Variations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ProviderId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OperatorId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Variations");
        }
    }
}
