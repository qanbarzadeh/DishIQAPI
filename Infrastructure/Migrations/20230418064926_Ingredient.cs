using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ingredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                schema: "User",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                schema: "User",
                newName: "Ingredient",
                newSchema: "Recipe");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                schema: "Recipe",
                table: "Ingredient",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                schema: "Recipe",
                table: "Ingredient",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Recipe",
                table: "Ingredient",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                schema: "Recipe",
                table: "Ingredient",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                schema: "Recipe",
                newName: "Ingredients",
                newSchema: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                schema: "User",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                schema: "User",
                table: "Ingredients",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "User",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                schema: "User",
                table: "Ingredients",
                column: "Id");
        }
    }
}
