using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Money_AmountId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "ItemProductSelections");

            migrationBuilder.DropTable(
                name: "Money");

            migrationBuilder.DropIndex(
                name: "IX_Payments_AmountId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles");

            migrationBuilder.DropColumn(
                name: "Flags",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AmountId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PayerCustomerId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "StripeChargeId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentIds",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmployeeRoles",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles",
                columns: new[] { "EmployeeId", "RoleId" });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Variations",
                columns: table => new
                {
                    VariationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PriceDifference = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variations", x => x.VariationId);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemVariationSelections",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    VariationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemVariationSelections", x => new { x.ItemId, x.VariationId });
                    table.ForeignKey(
                        name: "FK_ItemVariationSelections_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemVariationSelections_Variations_VariationId",
                        column: x => x.VariationId,
                        principalTable: "Variations",
                        principalColumn: "VariationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoles_RoleId",
                table: "EmployeeRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemVariationSelections_VariationId",
                table: "ItemVariationSelections",
                column: "VariationId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoles_Roles_RoleId",
                table: "EmployeeRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoles_Roles_RoleId",
                table: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "ItemVariationSelections");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Variations");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeRoles_RoleId",
                table: "EmployeeRoles");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EmployeeRoles",
                newName: "Id");

            migrationBuilder.AddColumn<List<string>>(
                name: "Flags",
                table: "Roles",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "AmountId",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PayerCustomerId",
                table: "Payments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeChargeId",
                table: "Payments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "PaymentIds",
                table: "Orders",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EmployeeRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ItemProductSelections",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemProductSelections", x => new { x.ItemId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ItemProductSelections_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemProductSelections_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Money",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Money", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AmountId",
                table: "Payments",
                column: "AmountId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemProductSelections_ProductId",
                table: "ItemProductSelections",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Money_AmountId",
                table: "Payments",
                column: "AmountId",
                principalTable: "Money",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
