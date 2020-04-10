using Microsoft.EntityFrameworkCore.Migrations;

namespace ShappingList.Migrations
{
    public partial class RolesCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemLists_ItemListId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemListId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemListId",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ListId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ListId",
                table: "Items",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemLists_ListId",
                table: "Items",
                column: "ListId",
                principalTable: "ItemLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemLists_ListId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ListId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "ItemListId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemListId",
                table: "Items",
                column: "ItemListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemLists_ItemListId",
                table: "Items",
                column: "ItemListId",
                principalTable: "ItemLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
