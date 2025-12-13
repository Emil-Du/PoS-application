using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovedVariationFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Variations_Items_ItemId",
                table: "Variations");

            migrationBuilder.DropIndex(
                name: "IX_Variations_ItemId",
                table: "Variations");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Variations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Variations_Items_ItemId",
                table: "Variations",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");
        }
    }
}
