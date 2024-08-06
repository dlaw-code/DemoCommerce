using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Commerce.ShoppingAPI.Migrations
{
    /// <inheritdoc />
    public partial class CartHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartelHeaders_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartelHeaders",
                table: "CartelHeaders");

            migrationBuilder.RenameTable(
                name: "CartelHeaders",
                newName: "CartHeaders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartHeaders",
                table: "CartHeaders",
                column: "CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "CartHeaderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartHeaders",
                table: "CartHeaders");

            migrationBuilder.RenameTable(
                name: "CartHeaders",
                newName: "CartelHeaders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartelHeaders",
                table: "CartelHeaders",
                column: "CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartelHeaders_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId",
                principalTable: "CartelHeaders",
                principalColumn: "CartHeaderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
