using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecipeIngredientConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                schema: "User",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                schema: "User",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                schema: "User",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                schema: "User",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_DietaryPreferences_User_DietaryPreferenceID",
                schema: "User",
                table: "DietaryPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_GeneratedRecipe_Id",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActivityLog_User_UserId",
                schema: "User",
                table: "UserActivityLog");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_User_UserId",
                schema: "User",
                table: "UserAllergy");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCookingSkillLevel_User_UserId",
                schema: "User",
                table: "UserCookingSkillLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_User_UserId",
                schema: "User",
                table: "UserNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileInfo_User_UserId",
                schema: "User",
                table: "UserProfileInfo");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "User");

            migrationBuilder.DropTable(
                name: "BloodTypes",
                schema: "User");

            migrationBuilder.DropTable(
                name: "CookingStep",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "CookingTechnique",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "Dislike",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "Flavor",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "FoodInformation",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "MealTime",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "MealType",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "RecipeDietPreference",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "User",
                schema: "User");

            migrationBuilder.DropTable(
                name: "GeneratedRecipe",
                schema: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_UserNotification_UserId",
                schema: "User",
                table: "UserNotification");

            migrationBuilder.DropIndex(
                name: "IX_UserCookingSkillLevel_UserId",
                schema: "User",
                table: "UserCookingSkillLevel");

            migrationBuilder.DropIndex(
                name: "IX_UserAllergy_UserId",
                schema: "User",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_UserActivityLog_UserId",
                schema: "User",
                table: "UserActivityLog");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "SocialMediaHandles",
                schema: "User",
                newName: "SocialMediaHandles",
                newSchema: "Recipe");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "User",
                newName: "AspNetUserTokens",
                newSchema: "Recipe");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "User",
                newName: "AspNetUserRoles",
                newSchema: "Recipe");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "User",
                newName: "AspNetUserLogins",
                newSchema: "Recipe");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "User",
                newName: "AspNetUserClaims",
                newSchema: "Recipe");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "User",
                newName: "AspNetRoles",
                newSchema: "Recipe");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "User",
                newName: "AspNetRoleClaims",
                newSchema: "Recipe");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "User",
                table: "UserProfileInfo",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "User",
                table: "UserProfileInfo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "User",
                table: "UserCookingSkillLevel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "User",
                table: "UserAllergy",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Recipe",
                table: "Ingredient",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "AllergyRestrictions",
                schema: "Recipe",
                table: "Ingredient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DietaryPreferences",
                schema: "Recipe",
                table: "Ingredient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "User",
                table: "DietaryPreferences",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DietaryPreferenceID",
                schema: "User",
                table: "DietaryPreferences",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NutritionInformation",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Carbohydrate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Protein = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Fat = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    VitaminA = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    VitaminC = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    VitaminD = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Calcium = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Iron = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Sodium = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionInformation_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "Recipe",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreparationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CookingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false),
                    ServingSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuisine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DishType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CookingMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CaloriesPerServing = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEvent",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEvent_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "User",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                schema: "Recipe",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => new { x.RecipeId, x.IngredientId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "User",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "Recipe",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "Recipe",
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "User",
                table: "ApplicationUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "User",
                table: "ApplicationUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionInformation_IngredientId",
                schema: "Recipe",
                table: "NutritionInformation",
                column: "IngredientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_ApplicationUserId",
                schema: "Recipe",
                table: "RecipeIngredient",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                schema: "Recipe",
                table: "RecipeIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                schema: "Recipe",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvent_ApplicationUserId",
                schema: "Recipe",
                table: "UserEvent",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_ApplicationUser_UserId",
                schema: "Recipe",
                table: "AspNetUserClaims",
                column: "UserId",
                principalSchema: "User",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_ApplicationUser_UserId",
                schema: "Recipe",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "User",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_ApplicationUser_UserId",
                schema: "Recipe",
                table: "AspNetUserRoles",
                column: "UserId",
                principalSchema: "User",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_ApplicationUser_UserId",
                schema: "Recipe",
                table: "AspNetUserTokens",
                column: "UserId",
                principalSchema: "User",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_ApplicationUser_UserId",
                schema: "Recipe",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_ApplicationUser_UserId",
                schema: "Recipe",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_ApplicationUser_UserId",
                schema: "Recipe",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_ApplicationUser_UserId",
                schema: "Recipe",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "NutritionInformation",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "RecipeIngredient",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "UserEvent",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "Recipes",
                schema: "Recipe");

            migrationBuilder.DropTable(
                name: "ApplicationUser",
                schema: "User");

            migrationBuilder.DropColumn(
                name: "AllergyRestrictions",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "DietaryPreferences",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "SocialMediaHandles",
                schema: "Recipe",
                newName: "SocialMediaHandles",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "Recipe",
                newName: "AspNetUserTokens",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "Recipe",
                newName: "AspNetUserRoles",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "Recipe",
                newName: "AspNetUserLogins",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "Recipe",
                newName: "AspNetUserClaims",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "Recipe",
                newName: "AspNetRoles",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "Recipe",
                newName: "AspNetRoleClaims",
                newSchema: "User");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "User",
                table: "UserProfileInfo",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "User",
                table: "UserProfileInfo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "User",
                table: "UserCookingSkillLevel",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "User",
                table: "UserAllergy",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Recipe",
                table: "Ingredient",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                schema: "Recipe",
                table: "Ingredient",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "Recipe",
                table: "Ingredient",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "User",
                table: "DietaryPreferences",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DietaryPreferenceID",
                schema: "User",
                table: "DietaryPreferences",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodTypes",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodTypeName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CookingTechnique",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingTechnique", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dislike",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dislike", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flavor",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlavorType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flavor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneratedRecipe",
                schema: "Recipe",
                columns: table => new
                {
                    GeneratedRecipeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedRecipe", x => x.GeneratedRecipeID);
                });

            migrationBuilder.CreateTable(
                name: "MealTime",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealTimeEnum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealType",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealName = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeDietPreference",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DietaryPreferences = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDietPreference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CookingStep",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingStep_GeneratedRecipe_Id",
                        column: x => x.Id,
                        principalSchema: "Recipe",
                        principalTable: "GeneratedRecipe",
                        principalColumn: "GeneratedRecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodInformation",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AllergyRestrictions = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CaloriesPerServing = table.Column<int>(type: "int", nullable: false),
                    CookingMethod = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CookingTime = table.Column<int>(type: "int", nullable: false),
                    Cuisine = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DietaryPreferences = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DishType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    KeyIngredients = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PreparationTime = table.Column<TimeSpan>(type: "Time", nullable: false),
                    ServingSize = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodInformation_GeneratedRecipe_Id",
                        column: x => x.Id,
                        principalSchema: "Recipe",
                        principalTable: "GeneratedRecipe",
                        principalColumn: "GeneratedRecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Recipe",
                table: "MealTime",
                columns: new[] { "Id", "MealTimeEnum" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                schema: "Recipe",
                table: "MealType",
                columns: new[] { "Id", "MealName" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 }
                });

            migrationBuilder.InsertData(
                schema: "Recipe",
                table: "RecipeDietPreference",
                columns: new[] { "Id", "DietaryPreferences" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 }
                });

            migrationBuilder.InsertData(
                schema: "Recipe",
                table: "Region",
                columns: new[] { "Id", "RegionName" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotification_UserId",
                schema: "User",
                table: "UserNotification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCookingSkillLevel_UserId",
                schema: "User",
                table: "UserCookingSkillLevel",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_UserId",
                schema: "User",
                table: "UserAllergy",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityLog_UserId",
                schema: "User",
                table: "UserActivityLog",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "User",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "User",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                schema: "User",
                table: "AspNetUserClaims",
                column: "UserId",
                principalSchema: "User",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                schema: "User",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "User",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                schema: "User",
                table: "AspNetUserRoles",
                column: "UserId",
                principalSchema: "User",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                schema: "User",
                table: "AspNetUserTokens",
                column: "UserId",
                principalSchema: "User",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietaryPreferences_User_DietaryPreferenceID",
                schema: "User",
                table: "DietaryPreferences",
                column: "DietaryPreferenceID",
                principalSchema: "User",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_GeneratedRecipe_Id",
                schema: "Recipe",
                table: "Ingredient",
                column: "Id",
                principalSchema: "Recipe",
                principalTable: "GeneratedRecipe",
                principalColumn: "GeneratedRecipeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivityLog_User_UserId",
                schema: "User",
                table: "UserActivityLog",
                column: "UserId",
                principalSchema: "User",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergy_User_UserId",
                schema: "User",
                table: "UserAllergy",
                column: "UserId",
                principalSchema: "User",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCookingSkillLevel_User_UserId",
                schema: "User",
                table: "UserCookingSkillLevel",
                column: "UserId",
                principalSchema: "User",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_User_UserId",
                schema: "User",
                table: "UserNotification",
                column: "UserId",
                principalSchema: "User",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileInfo_User_UserId",
                schema: "User",
                table: "UserProfileInfo",
                column: "UserId",
                principalSchema: "User",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
