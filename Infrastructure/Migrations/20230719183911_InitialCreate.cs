using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Dishiq");

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                schema: "Dishiq",
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
                name: "AspNetRoles",
                schema: "Dishiq",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietaryPreferences",
                schema: "Dishiq",
                columns: table => new
                {
                    DietaryPreferenceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowedDietaryPreferences = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryPreferences", x => x.DietaryPreferenceID);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                schema: "Dishiq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DietaryPreferences = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllergyRestrictions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaHandles",
                schema: "Dishiq",
                columns: table => new
                {
                    SocialMediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Handle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfileInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaHandles", x => x.SocialMediaId);
                });

            migrationBuilder.CreateTable(
                name: "UserActivityLog",
                schema: "Dishiq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActivityDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IPAddress = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeviceOS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrowserType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrowserVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivityLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserNotification",
                schema: "Dishiq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NotificationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NotificationText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "Dishiq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Dishiq",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "Dishiq",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Dishiq",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "Dishiq",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Dishiq",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                schema: "Dishiq",
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
                        principalSchema: "Dishiq",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEvent",
                schema: "Dishiq",
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
                        principalSchema: "Dishiq",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "Dishiq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Dishiq",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "Dishiq",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Dishiq",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Dishiq",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionInformation",
                schema: "Dishiq",
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
                        principalSchema: "Dishiq",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                schema: "Dishiq",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Dishiq",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "Dishiq",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "Dishiq",
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Dishiq",
                table: "ApplicationUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Dishiq",
                table: "ApplicationUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "Dishiq",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Dishiq",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "Dishiq",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "Dishiq",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "Dishiq",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionInformation_IngredientId",
                schema: "Dishiq",
                table: "NutritionInformation",
                column: "IngredientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_ApplicationUserId",
                schema: "Dishiq",
                table: "RecipeIngredient",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                schema: "Dishiq",
                table: "RecipeIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                schema: "Dishiq",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvent_ApplicationUserId",
                schema: "Dishiq",
                table: "UserEvent",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "DietaryPreferences",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "NutritionInformation",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "RecipeIngredient",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "SocialMediaHandles",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "UserActivityLog",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "UserEvent",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "UserNotification",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "Ingredient",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "Recipes",
                schema: "Dishiq");

            migrationBuilder.DropTable(
                name: "ApplicationUser",
                schema: "Dishiq");
        }
    }
}
