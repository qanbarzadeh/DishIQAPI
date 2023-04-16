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
                name: "User");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DietaryPreferences",
                schema: "User",
                columns: table => new
                {
                    DietaryPreferenceID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AllowedDietaryPreferences = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryPreferences", x => x.DietaryPreferenceID);
                    table.ForeignKey(
                        name: "FK_DietaryPreferences_User_DietaryPreferenceID",
                        column: x => x.DietaryPreferenceID,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserActivityLog",
                schema: "User",
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
                    table.ForeignKey(
                        name: "FK_UserActivityLog_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAllergy",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SeverityLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAllergy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAllergy_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCookingSkillLevel",
                schema: "User",
                columns: table => new
                {
                    SkillLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CookingSkillLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCookingSkillLevel", x => x.SkillLevelId);
                    table.ForeignKey(
                        name: "FK_UserCookingSkillLevel_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentials",
                schema: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    LastLoginDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AccountCreationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordResetExpirationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredentials", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserCredentials_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNotification",
                schema: "User",
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
                    table.ForeignKey(
                        name: "FK_UserNotification_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileInfo",
                schema: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastLoginDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AccountCreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SocialMediaHandle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LanguagePreference = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NotificationSettings = table.Column<bool>(type: "bit", nullable: false),
                    SubscriptionStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentInformation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserActivityLog = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsSuspicious = table.Column<bool>(type: "bit", nullable: false),
                    IsBlacklisted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileInfo", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfileInfo_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaHandles",
                schema: "User",
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
                    table.ForeignKey(
                        name: "FK_SocialMediaHandles_UserProfileInfo_UserProfileInfoId",
                        column: x => x.UserProfileInfoId,
                        principalSchema: "User",
                        principalTable: "UserProfileInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaHandles_UserProfileInfoId",
                schema: "User",
                table: "SocialMediaHandles",
                column: "UserProfileInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityLog_UserId",
                schema: "User",
                table: "UserActivityLog",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_UserId",
                schema: "User",
                table: "UserAllergy",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCookingSkillLevel_UserId",
                schema: "User",
                table: "UserCookingSkillLevel",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotification_UserId",
                schema: "User",
                table: "UserNotification",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaryPreferences",
                schema: "User");

            migrationBuilder.DropTable(
                name: "SocialMediaHandles",
                schema: "User");

            migrationBuilder.DropTable(
                name: "UserActivityLog",
                schema: "User");

            migrationBuilder.DropTable(
                name: "UserAllergy",
                schema: "User");

            migrationBuilder.DropTable(
                name: "UserCookingSkillLevel",
                schema: "User");

            migrationBuilder.DropTable(
                name: "UserCredentials",
                schema: "User");

            migrationBuilder.DropTable(
                name: "UserNotification",
                schema: "User");

            migrationBuilder.DropTable(
                name: "UserProfileInfo",
                schema: "User");

            migrationBuilder.DropTable(
                name: "User",
                schema: "User");
        }
    }
}
