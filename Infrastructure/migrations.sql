IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Authentication') IS NULL EXEC(N'CREATE SCHEMA [Authentication];');
GO

IF SCHEMA_ID(N'User') IS NULL EXEC(N'CREATE SCHEMA [User];');
GO

IF SCHEMA_ID(N'Recipe') IS NULL EXEC(N'CREATE SCHEMA [Recipe];');
GO

CREATE TABLE [Authentication].[AuthUser] (
    [Id] uniqueidentifier NOT NULL,
    [EmailAddress] nvarchar(255) NOT NULL,
    [Username] nvarchar(50) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    [UpdatedAt] datetime2 NOT NULL,
    [Version] int NOT NULL,
    CONSTRAINT [PK_AuthUser] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [User].[BloodTypes] (
    [Id] int NOT NULL IDENTITY,
    [BloodTypeName] int NOT NULL,
    CONSTRAINT [PK_BloodTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Recipe].[CookingTechnique] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_CookingTechnique] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Recipe].[Country] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(128) NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Recipe].[Dislike] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NULL,
    CONSTRAINT [PK_Dislike] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Recipe].[Flavor] (
    [Id] int NOT NULL IDENTITY,
    [FlavorType] int NOT NULL,
    CONSTRAINT [PK_Flavor] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Recipe].[GeneratedRecipe] (
    [GeneratedRecipeID] int NOT NULL IDENTITY,
    [FoodInformationId] int NOT NULL,
    CONSTRAINT [PK_GeneratedRecipe] PRIMARY KEY ([GeneratedRecipeID])
);
GO

CREATE TABLE [Recipe].[MealTime] (
    [Id] int NOT NULL IDENTITY,
    [MealTimeEnum] int NOT NULL,
    CONSTRAINT [PK_MealTime] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Recipe].[MealType] (
    [Id] int NOT NULL IDENTITY,
    [MealName] int NOT NULL,
    CONSTRAINT [PK_MealType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Recipe].[RecipeDietPreference] (
    [Id] int NOT NULL IDENTITY,
    [DietaryPreferences] int NOT NULL,
    CONSTRAINT [PK_RecipeDietPreference] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Recipe].[Region] (
    [Id] int NOT NULL IDENTITY,
    [RegionName] int NOT NULL,
    CONSTRAINT [PK_Region] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [User].[User] (
    [UserId] int NOT NULL IDENTITY,
    [Username] nvarchar(50) NOT NULL,
    [EmailAddress] varchar(100) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [Authentication].[ExternalLogin] (
    [Id] int NOT NULL IDENTITY,
    [LoginProvider] nvarchar(50) NOT NULL,
    [ProviderKey] nvarchar(255) NOT NULL,
    [AuthUserId] uniqueidentifier NOT NULL,
    [LinkedAt] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_ExternalLogin] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ExternalLogin_AuthUser_AuthUserId] FOREIGN KEY ([AuthUserId]) REFERENCES [Authentication].[AuthUser] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Authentication].[UserEvent] (
    [Id] int NOT NULL IDENTITY,
    [AuthUserId] uniqueidentifier NOT NULL,
    [EventType] int NOT NULL,
    [EventDate] datetime2 NOT NULL,
    CONSTRAINT [PK_UserEvent] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserEvent_AuthUser_AuthUserId] FOREIGN KEY ([AuthUserId]) REFERENCES [Authentication].[AuthUser] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Recipe].[CookingStep] (
    [Id] int NOT NULL,
    [Description] nvarchar(1000) NOT NULL,
    [Order] int NOT NULL DEFAULT 0,
    CONSTRAINT [PK_CookingStep] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CookingStep_GeneratedRecipe_Id] FOREIGN KEY ([Id]) REFERENCES [Recipe].[GeneratedRecipe] ([GeneratedRecipeID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Recipe].[FoodInformation] (
    [Id] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(256) NOT NULL,
    [PreparationTime] Time NOT NULL,
    [CookingTime] int NOT NULL,
    [Servings] int NOT NULL,
    [CaloriesPerServing] int NOT NULL,
    [ServingSize] int NOT NULL,
    [DietaryPreferences] nvarchar(64) NOT NULL,
    [KeyIngredients] nvarchar(64) NOT NULL,
    [AllergyRestrictions] nvarchar(64) NOT NULL,
    [Cuisine] nvarchar(64) NOT NULL,
    [DishType] nvarchar(64) NOT NULL,
    [CookingMethod] nvarchar(64) NOT NULL,
    CONSTRAINT [PK_FoodInformation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FoodInformation_GeneratedRecipe_Id] FOREIGN KEY ([Id]) REFERENCES [Recipe].[GeneratedRecipe] ([GeneratedRecipeID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Recipe].[Ingredient] (
    [Id] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Unit] nvarchar(50) NOT NULL,
    [Quantity] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Ingredient] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Ingredient_GeneratedRecipe_Id] FOREIGN KEY ([Id]) REFERENCES [Recipe].[GeneratedRecipe] ([GeneratedRecipeID]) ON DELETE CASCADE
);
GO

CREATE TABLE [User].[DietaryPreferences] (
    [DietaryPreferenceID] int NOT NULL,
    [UserId] int NOT NULL,
    [AllowedDietaryPreferences] int NOT NULL,
    CONSTRAINT [PK_DietaryPreferences] PRIMARY KEY ([DietaryPreferenceID]),
    CONSTRAINT [FK_DietaryPreferences_User_DietaryPreferenceID] FOREIGN KEY ([DietaryPreferenceID]) REFERENCES [User].[User] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [User].[UserActivityLog] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ActivityType] nvarchar(50) NOT NULL,
    [ActivityDate] datetimeoffset NOT NULL,
    [IPAddress] varchar(15) NOT NULL,
    [DeviceType] nvarchar(50) NOT NULL,
    [DeviceOS] nvarchar(50) NOT NULL,
    [BrowserType] nvarchar(50) NOT NULL,
    [BrowserVersion] nvarchar(50) NOT NULL,
    [Location] nvarchar(100) NOT NULL,
    [Duration] int NULL,
    CONSTRAINT [PK_UserActivityLog] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserActivityLog_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User].[User] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [User].[UserAllergy] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [SeverityLevel] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getutcdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_UserAllergy] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserAllergy_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User].[User] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [User].[UserCookingSkillLevel] (
    [SkillLevelId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [CookingSkillLevel] int NOT NULL,
    CONSTRAINT [PK_UserCookingSkillLevel] PRIMARY KEY ([SkillLevelId]),
    CONSTRAINT [FK_UserCookingSkillLevel_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User].[User] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [User].[UserCredentials] (
    [UserId] int NOT NULL,
    [Username] nvarchar(50) NOT NULL,
    [EmailAddress] nvarchar(100) NOT NULL,
    [Password] nvarchar(255) NOT NULL,
    [AccountStatus] int NOT NULL,
    [LastLoginDateTime] datetimeoffset NOT NULL,
    [AccountCreationDateTime] datetimeoffset NOT NULL,
    [PasswordResetToken] nvarchar(255) NOT NULL,
    [PasswordResetExpirationDateTime] datetimeoffset NULL,
    CONSTRAINT [PK_UserCredentials] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_UserCredentials_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User].[User] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [User].[UserNotification] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [NotificationType] nvarchar(50) NOT NULL,
    [NotificationText] nvarchar(500) NOT NULL,
    [IsRead] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_UserNotification] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserNotification_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User].[User] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [User].[UserProfileInfo] (
    [UserId] int NOT NULL,
    [Username] nvarchar(max) NOT NULL,
    [EmailAddress] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [FullName] nvarchar(100) NOT NULL,
    [Gender] int NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [ProfilePicture] nvarchar(255) NOT NULL,
    [Bio] nvarchar(500) NOT NULL,
    [Location] nvarchar(100) NOT NULL,
    [LastLoginDate] datetimeoffset NOT NULL,
    [AccountCreationDate] datetimeoffset NOT NULL,
    [IsEmailVerified] bit NOT NULL,
    [PhoneNumber] nvarchar(20) NOT NULL,
    [SocialMediaHandle] nvarchar(500) NOT NULL,
    [LanguagePreference] nvarchar(20) NOT NULL,
    [NotificationSettings] bit NOT NULL,
    [SubscriptionStatus] nvarchar(20) NOT NULL,
    [PaymentInformation] nvarchar(500) NOT NULL,
    [UserActivityLog] nvarchar(500) NOT NULL,
    [IsSuspicious] bit NOT NULL,
    [IsBlacklisted] bit NOT NULL,
    CONSTRAINT [PK_UserProfileInfo] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_UserProfileInfo_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User].[User] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [User].[SocialMediaHandles] (
    [SocialMediaId] int NOT NULL IDENTITY,
    [Type] int NOT NULL,
    [Handle] nvarchar(max) NOT NULL,
    [UserProfileInfoId] int NOT NULL,
    CONSTRAINT [PK_SocialMediaHandles] PRIMARY KEY ([SocialMediaId]),
    CONSTRAINT [FK_SocialMediaHandles_UserProfileInfo_UserProfileInfoId] FOREIGN KEY ([UserProfileInfoId]) REFERENCES [User].[UserProfileInfo] ([UserId]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MealTimeEnum') AND [object_id] = OBJECT_ID(N'[Recipe].[MealTime]'))
    SET IDENTITY_INSERT [Recipe].[MealTime] ON;
INSERT INTO [Recipe].[MealTime] ([Id], [MealTimeEnum])
VALUES (1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MealTimeEnum') AND [object_id] = OBJECT_ID(N'[Recipe].[MealTime]'))
    SET IDENTITY_INSERT [Recipe].[MealTime] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MealName') AND [object_id] = OBJECT_ID(N'[Recipe].[MealType]'))
    SET IDENTITY_INSERT [Recipe].[MealType] ON;
INSERT INTO [Recipe].[MealType] ([Id], [MealName])
VALUES (1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MealName') AND [object_id] = OBJECT_ID(N'[Recipe].[MealType]'))
    SET IDENTITY_INSERT [Recipe].[MealType] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DietaryPreferences') AND [object_id] = OBJECT_ID(N'[Recipe].[RecipeDietPreference]'))
    SET IDENTITY_INSERT [Recipe].[RecipeDietPreference] ON;
INSERT INTO [Recipe].[RecipeDietPreference] ([Id], [DietaryPreferences])
VALUES (1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DietaryPreferences') AND [object_id] = OBJECT_ID(N'[Recipe].[RecipeDietPreference]'))
    SET IDENTITY_INSERT [Recipe].[RecipeDietPreference] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RegionName') AND [object_id] = OBJECT_ID(N'[Recipe].[Region]'))
    SET IDENTITY_INSERT [Recipe].[Region] ON;
INSERT INTO [Recipe].[Region] ([Id], [RegionName])
VALUES (1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RegionName') AND [object_id] = OBJECT_ID(N'[Recipe].[Region]'))
    SET IDENTITY_INSERT [Recipe].[Region] OFF;
GO

CREATE INDEX [IX_ExternalLogin_AuthUserId] ON [Authentication].[ExternalLogin] ([AuthUserId]);
GO

CREATE INDEX [IX_SocialMediaHandles_UserProfileInfoId] ON [User].[SocialMediaHandles] ([UserProfileInfoId]);
GO

CREATE UNIQUE INDEX [IX_UserActivityLog_UserId] ON [User].[UserActivityLog] ([UserId]);
GO

CREATE INDEX [IX_UserAllergy_UserId] ON [User].[UserAllergy] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_UserCookingSkillLevel_UserId] ON [User].[UserCookingSkillLevel] ([UserId]);
GO

CREATE INDEX [IX_UserEvent_AuthUserId] ON [Authentication].[UserEvent] ([AuthUserId]);
GO

CREATE INDEX [IX_UserNotification_UserId] ON [User].[UserNotification] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230626144153_InitialCreate', N'7.0.7');
GO

COMMIT;
GO

