using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flower.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeflower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoseCategories_Roses_FlowerId",
                table: "RoseCategories");

            migrationBuilder.RenameColumn(
                name: "FlowerId",
                table: "RoseCategories",
                newName: "RoseId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoseCategories_Roses_RoseId",
                table: "RoseCategories",
                column: "RoseId",
                principalTable: "Roses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoseCategories_Roses_RoseId",
                table: "RoseCategories");

            migrationBuilder.RenameColumn(
                name: "RoseId",
                table: "RoseCategories",
                newName: "FlowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoseCategories_Roses_FlowerId",
                table: "RoseCategories",
                column: "FlowerId",
                principalTable: "Roses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
