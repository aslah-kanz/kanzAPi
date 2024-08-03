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

IF SCHEMA_ID(N'Product') IS NULL EXEC(N'CREATE SCHEMA [Product];');
GO

IF SCHEMA_ID(N'Common') IS NULL EXEC(N'CREATE SCHEMA [Common];');
GO

IF SCHEMA_ID(N'Transaction') IS NULL EXEC(N'CREATE SCHEMA [Transaction];');
GO

IF SCHEMA_ID(N'Account') IS NULL EXEC(N'CREATE SCHEMA [Account];');
GO

IF SCHEMA_ID(N'Logging') IS NULL EXEC(N'CREATE SCHEMA [Logging];');
GO

IF SCHEMA_ID(N'Security') IS NULL EXEC(N'CREATE SCHEMA [Security];');
GO

CREATE TABLE [Common].[Currency] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Description] nvarchar(125) NOT NULL,
    [Country] nvarchar(125) NOT NULL,
    [Symbol] nvarchar(20) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Common].[Document] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Url] nvarchar(255) NOT NULL,
    [Type] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Document] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Logging].[ErrorLog] (
    [Id] bigint NOT NULL IDENTITY,
    [Type] nvarchar(255) NOT NULL,
    [Message] nvarchar(255) NULL,
    [Details] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Common].[FaqGroup] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [ShowAtHomePage] bit NOT NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_FaqGroup] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Common].[Image] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Url] nvarchar(255) NOT NULL,
    [Width] int NOT NULL,
    [Height] int NOT NULL,
    [Type] nvarchar(20) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Logging].[InternalRequestLog] (
    [Id] bigint NOT NULL IDENTITY,
    [Method] nvarchar(10) NOT NULL,
    [Path] nvarchar(255) NOT NULL,
    [QueryString] nvarchar(255) NULL,
    [Headers] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    CONSTRAINT [PK_InternalRequestLog] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Logging].[InternalResponseLog] (
    [Id] bigint NOT NULL IDENTITY,
    [RequestId] bigint NULL,
    [Headers] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    CONSTRAINT [PK_InternalResponseLog] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Common].[JobField] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_JobField] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Account].[Privilege] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Privilege] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Logging].[RequestLog] (
    [Id] bigint NOT NULL IDENTITY,
    [IpAddress] nvarchar(15) NULL,
    [Method] nvarchar(10) NOT NULL,
    [Path] nvarchar(255) NOT NULL,
    [QueryString] nvarchar(255) NULL,
    [Headers] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    CONSTRAINT [PK_RequestLog] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Account].[Role] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Transaction].[ShippingMethod] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(100) NULL,
    [ProviderName] nvarchar(100) NOT NULL,
    [DeliveryCompanyName] nvarchar(100) NOT NULL,
    [DeliveryEstimateTime] nvarchar(100) NOT NULL,
    [Detail] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_ShippingMethod] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Common].[Subscriber] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [Email] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Subscriber] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Common].[Suggestion] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [Name] nvarchar(100) NULL,
    [Email] nvarchar(100) NULL,
    [PhoneNumber] nvarchar(65) NULL,
    [Subject] nvarchar(255) NULL,
    [Message] nvarchar(255) NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Suggestion] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Common].[Faq] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [FaqGroupId] int NOT NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Answer] nvarchar(max) NULL,
    [Question] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Faq] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Faq_FaqGroup_FaqGroupId] FOREIGN KEY ([FaqGroupId]) REFERENCES [Common].[FaqGroup] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Common].[Banner] (
    [Id] int NOT NULL IDENTITY,
    [ImageId] bigint NULL,
    [Code] nvarchar(20) NULL,
    [Url] nvarchar(255) NOT NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Banner] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Banner_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Common].[Blog] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [Slug] nvarchar(100) NULL,
    [ImageId] bigint NULL,
    [MetaDescription] nvarchar(160) NULL,
    [MetaKeyword] nvarchar(65) NULL,
    [ReadCount] int NOT NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Blog] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Blog_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Product].[Brand] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [NameEn] nvarchar(160) NOT NULL,
    [Slug] nvarchar(100) NOT NULL,
    [ImageId] bigint NULL,
    [BwImageId] bigint NULL,
    [MetaKeyword] nvarchar(65) NULL,
    [MetaDescription] nvarchar(160) NULL,
    [ShowAtHomePage] bit NOT NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Brand] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Brand_Image_BwImageId] FOREIGN KEY ([BwImageId]) REFERENCES [Common].[Image] ([Id]),
    CONSTRAINT [FK_Brand_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Common].[Catalogue] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [Slug] nvarchar(100) NULL,
    [ImageId] bigint NULL,
    [DocumentId] bigint NULL,
    [MetaDescription] nvarchar(160) NULL,
    [MetaKeyword] nvarchar(65) NULL,
    [ReadCount] int NOT NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Catalogue] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Catalogue_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Common].[Document] ([Id]),
    CONSTRAINT [FK_Catalogue_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Product].[Category] (
    [Id] int NOT NULL IDENTITY,
    [ParentId] int NULL,
    [Code] nvarchar(8) NULL,
    [Slug] nvarchar(100) NOT NULL,
    [ImageId] bigint NULL,
    [MetaKeyword] nvarchar(65) NULL,
    [MetaDescription] nvarchar(160) NULL,
    [ShowAtHomePage] bit NOT NULL,
    [Status] nvarchar(10) NOT NULL,
    [Path] nvarchar(255) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Category_Category_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [Product].[Category] ([Id]),
    CONSTRAINT [FK_Category_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Common].[Certificate] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [Slug] nvarchar(100) NULL,
    [ImageId] bigint NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Certificate] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Certificate_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Common].[Country] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(2) NOT NULL,
    [PhoneCode] int NOT NULL,
    [PhoneStartNumber] int NOT NULL,
    [PhoneMinLength] int NOT NULL,
    [PhoneMaxLength] int NOT NULL,
    [ImageId] bigint NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Country_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Common].[Language] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [ImageId] bigint NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Language_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Transaction].[PaymentMethod] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(100) NULL,
    [ImageId] bigint NULL,
    [ImageArId] bigint NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Instruction] nvarchar(max) NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PaymentMethod] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaymentMethod_Image_ImageArId] FOREIGN KEY ([ImageArId]) REFERENCES [Common].[Image] ([Id]),
    CONSTRAINT [FK_PaymentMethod_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Account].[Principal] (
    [Id] int NOT NULL IDENTITY,
    [ImageId] bigint NULL,
    [Username] nvarchar(100) NOT NULL,
    [Password] nvarchar(255) NULL,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [CountryCode] nvarchar(4) NULL,
    [PhoneNumber] nvarchar(15) NULL,
    [Type] nvarchar(20) NULL,
    [Gender] nvarchar(6) NULL,
    [BirthDate] date NULL,
    [AcceptNewsLetter] bit NOT NULL,
    [Comment] nvarchar(255) NULL,
    [Status] nvarchar(30) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Principal] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Principal_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Common].[WebPage] (
    [Id] int NOT NULL IDENTITY,
    [ImageId] bigint NULL,
    [Code] nvarchar(20) NULL,
    [Slug] nvarchar(100) NOT NULL,
    [ShowAtHomePage] bit NOT NULL,
    [MetaKeyword] nvarchar(65) NULL,
    [MetaDescription] nvarchar(160) NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Contents] nvarchar(max) NULL,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_WebPage] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebPage_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Common].[WebsiteProfile] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [Name] nvarchar(65) NULL,
    [MetaKeyword] nvarchar(65) NULL,
    [MetaDescription] nvarchar(160) NULL,
    [ImageId] bigint NULL,
    [FaviconId] bigint NULL,
    [Instagram] nvarchar(160) NULL,
    [Twitter] nvarchar(160) NULL,
    [Facebook] nvarchar(160) NULL,
    [Linkedin] nvarchar(160) NULL,
    [Youtube] nvarchar(160) NULL,
    [PhoneNumber] nvarchar(15) NULL,
    [Email] nvarchar(100) NULL,
    [Address] nvarchar(255) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_WebsiteProfile] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebsiteProfile_Image_FaviconId] FOREIGN KEY ([FaviconId]) REFERENCES [Common].[Image] ([Id]),
    CONSTRAINT [FK_WebsiteProfile_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id])
);
GO

CREATE TABLE [Common].[Job] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [Slug] nvarchar(100) NULL,
    [Requirement] nvarchar(255) NULL,
    [JobFieldId] int NOT NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Experience] nvarchar(max) NOT NULL,
    [JobLocation] nvarchar(max) NOT NULL,
    [JobType] nvarchar(max) NOT NULL,
    [Responsibility] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Job] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Job_JobField_JobFieldId] FOREIGN KEY ([JobFieldId]) REFERENCES [Common].[JobField] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[RolePrivilege] (
    [RoleId] int NOT NULL,
    [PrivilegeId] int NOT NULL,
    CONSTRAINT [PK_RolePrivilege] PRIMARY KEY ([PrivilegeId], [RoleId]),
    CONSTRAINT [FK_RolePrivilege_Privilege_PrivilegeId] FOREIGN KEY ([PrivilegeId]) REFERENCES [Account].[Privilege] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RolePrivilege_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Account].[Role] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Product].[Attribute] (
    [Id] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [BrandId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Options] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Attribute] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Attribute_Brand_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Product].[Brand] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Attribute_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Product].[Category] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Product].[BrandCategory] (
    [BrandId] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_BrandCategory] PRIMARY KEY ([BrandId], [CategoryId]),
    CONSTRAINT [FK_BrandCategory_Brand_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Product].[Brand] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BrandCategory_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Product].[Category] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[PrincipalDetail] (
    [Id] int NOT NULL IDENTITY,
    [PrincipalId] int NOT NULL,
    [CountryId] int NOT NULL,
    [City] nvarchar(125) NOT NULL,
    [CompanyNumber] nvarchar(125) NOT NULL,
    [CompanyNameEn] nvarchar(125) NOT NULL,
    [CompanyNameAr] nvarchar(125) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_PrincipalDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PrincipalDetail_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Common].[Country] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Account].[Notification] (
    [Id] uniqueidentifier NOT NULL,
    [PrincipalId] int NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [MessageArgs] nvarchar(max) NULL,
    [ImageId] bigint NULL,
    [Type] nvarchar(20) NULL,
    [ReferenceId] nvarchar(65) NULL,
    [ReadAt] datetime NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Notification] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notification_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id]),
    CONSTRAINT [FK_Notification_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Security].[OneTimeToken] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(20) NOT NULL,
    [Token] nvarchar(255) NOT NULL,
    [PrincipalId] int NOT NULL,
    [ExpiredAt] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_OneTimeToken] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OneTimeToken_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Security].[Otp] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [PrincipalId] int NOT NULL,
    [AttemptCount] int NOT NULL,
    [ExpiredAt] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Otp] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Otp_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[PrincipalAddress] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(100) NULL,
    [PrincipalId] int NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [RecipientName] nvarchar(255) NOT NULL,
    [CountryCode] nvarchar(4) NULL,
    [PhoneNumber] nvarchar(15) NULL,
    [Address] nvarchar(255) NOT NULL,
    [City] nvarchar(125) NOT NULL,
    [Country] nvarchar(125) NOT NULL,
    [Latitude] decimal(12,8) NOT NULL,
    [Longitude] decimal(13,8) NOT NULL,
    [DefaultAddress] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_PrincipalAddress] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PrincipalAddress_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[PrincipalBank] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(100) NULL,
    [PrincipalId] int NOT NULL,
    [DocumentId] bigint NULL,
    [CurrencyId] int NULL,
    [Name] nvarchar(125) NOT NULL,
    [City] nvarchar(125) NOT NULL,
    [PaymentMode] nvarchar(125) NOT NULL,
    [BeneficiaryName] nvarchar(125) NOT NULL,
    [Iban] nvarchar(125) NOT NULL,
    [AccountNumber] nvarchar(125) NOT NULL,
    [SwiftCode] nvarchar(125) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_PrincipalBank] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PrincipalBank_Currency_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PrincipalBank_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Common].[Document] ([Id]),
    CONSTRAINT [FK_PrincipalBank_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[PrincipalRole] (
    [PrincipalId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_PrincipalRole] PRIMARY KEY ([PrincipalId], [RoleId]),
    CONSTRAINT [FK_PrincipalRole_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Account].[Role] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[PrincipalWallet] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(100) NULL,
    [PrincipalId] int NOT NULL,
    [Type] nvarchar(6) NOT NULL,
    [ReferenceId] nvarchar(65) NULL,
    [Amount] decimal(18,2) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_PrincipalWallet] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PrincipalWallet_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Security].[RefreshToken] (
    [Id] int NOT NULL IDENTITY,
    [Token] nvarchar(255) NOT NULL,
    [AccessTokenId] nvarchar(255) NOT NULL,
    [PrincipalId] int NOT NULL,
    [ExpiredAt] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshToken_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[Store] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Code] nvarchar(100) NULL,
    [WarehouseId] nvarchar(20) NULL,
    [PrincipalId] int NOT NULL,
    [Address] nvarchar(255) NULL,
    [City] nvarchar(100) NULL,
    [Country] nvarchar(100) NULL,
    [Latitude] decimal(12,8) NOT NULL,
    [Longitude] decimal(13,8) NOT NULL,
    [SaleItemCount] int NOT NULL,
    [Status] nvarchar(30) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Store_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[Withdraw] (
    [Id] int NOT NULL IDENTITY,
    [PrincipalId] int NOT NULL,
    [ImageId] bigint NULL,
    [Amount] decimal(18,2) NULL,
    [Comment] nvarchar(255) NULL,
    [Status] nvarchar(10) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Withdraw] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Withdraw_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id]),
    CONSTRAINT [FK_Withdraw_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Common].[JobApplicant] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NULL,
    [JobId] int NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [PhoneNumber] nvarchar(65) NOT NULL,
    [DocumentId] bigint NULL,
    [Status] nvarchar(20) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_JobApplicant] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_JobApplicant_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Common].[Document] ([Id]),
    CONSTRAINT [FK_JobApplicant_Job_JobId] FOREIGN KEY ([JobId]) REFERENCES [Common].[Job] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[CompanyMember] (
    [PrincipalId] int NOT NULL,
    [PrincipalDetailId] int NOT NULL,
    CONSTRAINT [PK_CompanyMember] PRIMARY KEY ([PrincipalDetailId], [PrincipalId]),
    CONSTRAINT [FK_CompanyMember_PrincipalDetail_PrincipalDetailId] FOREIGN KEY ([PrincipalDetailId]) REFERENCES [Account].[PrincipalDetail] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CompanyMember_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[Department] (
    [Id] int NOT NULL IDENTITY,
    [PrincipalDetailId] int NOT NULL,
    [Code] nvarchar(20) NULL,
    [Name] nvarchar(255) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Department_PrincipalDetail_PrincipalDetailId] FOREIGN KEY ([PrincipalDetailId]) REFERENCES [Account].[PrincipalDetail] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[PrincipalDetailItem] (
    [Id] int NOT NULL IDENTITY,
    [PrincipalDetailId] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_PrincipalDetailItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PrincipalDetailItem_PrincipalDetail_PrincipalDetailId] FOREIGN KEY ([PrincipalDetailId]) REFERENCES [Account].[PrincipalDetail] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Product].[Product] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(100) NULL,
    [Mpn] nvarchar(100) NULL,
    [Slug] nvarchar(100) NULL,
    [FamilyCode] nvarchar(60) NULL,
    [GroupCode] nvarchar(60) NULL,
    [IconId] bigint NULL,
    [ImageId] bigint NULL,
    [BrandId] int NOT NULL,
    [PrincipalDetailId] int NULL,
    [Length] float NOT NULL,
    [Width] float NOT NULL,
    [Height] float NOT NULL,
    [Weight] float NOT NULL,
    [MetaKeyword] nvarchar(65) NULL,
    [MetaDescription] nvarchar(160) NULL,
    [OriginalPrice] decimal(18,2) NULL,
    [LowestPrice] decimal(18,2) NULL,
    [HighestPrice] decimal(18,2) NULL,
    [Sellable] bit NOT NULL,
    [Status] nvarchar(20) NOT NULL,
    [Comment] nvarchar(255) NULL,
    [SortOrder] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Gtin] nvarchar(max) NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_Brand_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Product].[Brand] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Product_Image_IconId] FOREIGN KEY ([IconId]) REFERENCES [Common].[Image] ([Id]),
    CONSTRAINT [FK_Product_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id]),
    CONSTRAINT [FK_Product_PrincipalDetail_PrincipalDetailId] FOREIGN KEY ([PrincipalDetailId]) REFERENCES [Account].[PrincipalDetail] ([Id])
);
GO

CREATE TABLE [Account].[PrincipalStore] (
    [PrincipalId] int NOT NULL,
    [StoreId] int NOT NULL,
    CONSTRAINT [PK_PrincipalStore] PRIMARY KEY ([PrincipalId], [StoreId]),
    CONSTRAINT [FK_PrincipalStore_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalStore_Store_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Account].[Store] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account].[PrincipalDepartment] (
    [PrincipalId] int NOT NULL,
    [DepartmentId] int NOT NULL,
    CONSTRAINT [PK_PrincipalDepartment] PRIMARY KEY ([DepartmentId], [PrincipalId]),
    CONSTRAINT [FK_PrincipalDepartment_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Account].[Department] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalDepartment_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transaction].[Cart] (
    [Id] int NOT NULL IDENTITY,
    [PrincipalId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Quantity] int NOT NULL,
    [Stock] int NOT NULL,
    [Comment] nvarchar(255) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cart_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Cart_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[CustomerOrder] (
    [Id] uniqueidentifier NOT NULL,
    [Code] nvarchar(100) NULL,
    [InvoiceNumber] nvarchar(100) NULL,
    [UrwayTrackId] nvarchar(30) NULL,
    [UrwayTransactionId] nvarchar(30) NULL,
    [PrincipalId] int NOT NULL,
    [PrincipalDetailId] int NULL,
    [AddressId] int NOT NULL,
    [PaymentMethodId] int NULL,
    [SubTotal] decimal(18,2) NOT NULL,
    [PromoCode] nvarchar(100) NULL,
    [DiscountPrice] decimal(18,2) NULL,
    [GrandTotal] decimal(18,2) NOT NULL,
    [Status] nvarchar(20) NOT NULL,
    [PurchaseQuoteStatus] nvarchar(20) NOT NULL,
    [HiglightedProductId] int NOT NULL,
    [DeliveryOptionId] int NULL,
    [EstimatedDeliveryCost] decimal(18,2) NULL,
    [DeliveryOptions] nvarchar(max) NOT NULL,
    [Comment] nvarchar(255) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_CustomerOrder] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerOrder_PaymentMethod_PaymentMethodId] FOREIGN KEY ([PaymentMethodId]) REFERENCES [Transaction].[PaymentMethod] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerOrder_PrincipalAddress_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Account].[PrincipalAddress] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerOrder_PrincipalDetail_PrincipalDetailId] FOREIGN KEY ([PrincipalDetailId]) REFERENCES [Account].[PrincipalDetail] ([Id]),
    CONSTRAINT [FK_CustomerOrder_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerOrder_Product_HiglightedProductId] FOREIGN KEY ([HiglightedProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[Inquiry] (
    [Id] int NOT NULL IDENTITY,
    [PrincipalId] int NOT NULL,
    [ProductId] int NOT NULL,
    [TotalPrice] decimal(18,2) NULL,
    [Quantity] int NOT NULL,
    [Comment] nvarchar(255) NULL,
    [Status] nvarchar(20) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Inquiry] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Inquiry_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Inquiry_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Product].[ProductCategory] (
    [ProductId] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_ProductCategory] PRIMARY KEY ([CategoryId], [ProductId]),
    CONSTRAINT [FK_ProductCategory_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Product].[Category] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductCategory_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Product].[ProductImage] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [ImageId] bigint NOT NULL,
    [SortOrder] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_ProductImage] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductImage_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductImage_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Product].[ProductProperty] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(100) NULL,
    [ProductId] int NOT NULL,
    [Type] nvarchar(10) NOT NULL,
    [SortOrder] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Fields] nvarchar(max) NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ProductProperty] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductProperty_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Product].[SaleItem] (
    [Id] bigint NOT NULL IDENTITY,
    [Code] nvarchar(100) NULL,
    [ProductId] int NOT NULL,
    [VendorSku] nvarchar(max) NULL,
    [BcId] nvarchar(max) NULL,
    [Stock] int NOT NULL,
    [ReservedStock] int NOT NULL,
    [MinPrice] decimal(18,2) NULL,
    [MaxPrice] decimal(18,2) NULL,
    [DiscountPrice] decimal(18,2) NULL,
    [Price] decimal(18,2) NOT NULL,
    [OriginalPrice] decimal(18,2) NULL,
    [MinOrderQuantity] int NULL,
    [MaxOrderQuantity] int NULL,
    [StoreId] int NOT NULL,
    [Enabled] bit NOT NULL,
    [Status] nvarchar(30) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_SaleItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SaleItem_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SaleItem_Store_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Account].[Store] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[WishList] (
    [Id] int NOT NULL IDENTITY,
    [PrincipalId] int NOT NULL,
    [ProductId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_WishList] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WishList_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_WishList_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[CustomerOrderItem] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerOrderId] uniqueidentifier NOT NULL,
    [CartId] int NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NULL,
    [SubTotal] decimal(18,2) NOT NULL,
    [Comment] nvarchar(255) NULL,
    [Status] nvarchar(20) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_CustomerOrderItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerOrderItem_CustomerOrder_CustomerOrderId] FOREIGN KEY ([CustomerOrderId]) REFERENCES [Transaction].[CustomerOrder] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerOrderItem_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[Shipping] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerOrderId] uniqueidentifier NOT NULL,
    [ShippingMethodId] int NOT NULL,
    [Cost] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Shipping] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Shipping_CustomerOrder_CustomerOrderId] FOREIGN KEY ([CustomerOrderId]) REFERENCES [Transaction].[CustomerOrder] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Shipping_ShippingMethod_ShippingMethodId] FOREIGN KEY ([ShippingMethodId]) REFERENCES [Transaction].[ShippingMethod] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[StoreOrder] (
    [Id] uniqueidentifier NOT NULL,
    [InvoiceNumber] nvarchar(100) NOT NULL,
    [OtoId] int NULL,
    [CustomerOrderId] uniqueidentifier NOT NULL,
    [StoreId] int NOT NULL,
    [PackageCount] int NULL,
    [DeliveryCost] decimal(18,2) NOT NULL,
    [ProductCount] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_StoreOrder] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StoreOrder_CustomerOrder_CustomerOrderId] FOREIGN KEY ([CustomerOrderId]) REFERENCES [Transaction].[CustomerOrder] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StoreOrder_Store_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Account].[Store] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Product].[ProductPropertyItem] (
    [Id] uniqueidentifier NOT NULL,
    [PropertyId] int NOT NULL,
    [ImageId] bigint NULL,
    [SortOrder] int NOT NULL,
    [SaveAsAttribute] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Unit1] nvarchar(max) NULL,
    [Unit2] nvarchar(max) NULL,
    [Unit3] nvarchar(max) NULL,
    [Value1] nvarchar(max) NOT NULL,
    [Value2] nvarchar(max) NULL,
    [Value3] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductPropertyItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductPropertyItem_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id]),
    CONSTRAINT [FK_ProductPropertyItem_ProductProperty_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Product].[ProductProperty] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transaction].[CartSaleItem] (
    [Id] int NOT NULL IDENTITY,
    [CartId] int NOT NULL,
    [SaleItemId] bigint NOT NULL,
    [StoreId] int NOT NULL,
    [ProductId] int NOT NULL,
    [MinPrice] decimal(18,2) NULL,
    [MaxPrice] decimal(18,2) NULL,
    [DiscountPrice] decimal(18,2) NULL,
    [Stock] int NOT NULL,
    [MinOrderQuantity] int NULL,
    [MaxOrderQuantity] int NULL,
    [VendorSku] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_CartSaleItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CartSaleItem_Cart_CartId] FOREIGN KEY ([CartId]) REFERENCES [Transaction].[Cart] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartSaleItem_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CartSaleItem_SaleItem_SaleItemId] FOREIGN KEY ([SaleItemId]) REFERENCES [Product].[SaleItem] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartSaleItem_Store_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Account].[Store] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[PurchaseQuote] (
    [Id] uniqueidentifier NOT NULL,
    [Code] nvarchar(100) NULL,
    [VendorSku] nvarchar(max) NULL,
    [CustomerOrderId] uniqueidentifier NOT NULL,
    [CustomerOrderItemId] uniqueidentifier NOT NULL,
    [StoreOrderId] uniqueidentifier NOT NULL,
    [CartId] int NULL,
    [SaleItemId] bigint NULL,
    [StoreId] int NOT NULL,
    [ProductId] int NOT NULL,
    [RequestedQuantity] int NOT NULL,
    [ConfirmedQuantity] int NULL,
    [TotalRequestedQuantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [MinPrice] decimal(18,2) NULL,
    [MaxPrice] decimal(18,2) NULL,
    [DiscountPrice] decimal(18,2) NULL,
    [Stock] int NOT NULL,
    [MinOrderQuantity] int NULL,
    [MaxOrderQuantity] int NULL,
    [SubTotal] decimal(18,2) NULL,
    [Tax] decimal(18,2) NULL,
    [PlatformCommission] decimal(18,2) NULL,
    [Comment] nvarchar(255) NULL,
    [Status] nvarchar(20) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_PurchaseQuote] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseQuote_CustomerOrderItem_CustomerOrderItemId] FOREIGN KEY ([CustomerOrderItemId]) REFERENCES [Transaction].[CustomerOrderItem] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PurchaseQuote_CustomerOrder_CustomerOrderId] FOREIGN KEY ([CustomerOrderId]) REFERENCES [Transaction].[CustomerOrder] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseQuote_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseQuote_StoreOrder_StoreOrderId] FOREIGN KEY ([StoreOrderId]) REFERENCES [Transaction].[StoreOrder] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseQuote_Store_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Account].[Store] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[Exchange] (
    [Id] uniqueidentifier NOT NULL,
    [Number] nvarchar(100) NULL,
    [PurchaseQuoteId] uniqueidentifier NOT NULL,
    [ProductId] int NOT NULL,
    [StoreId] int NOT NULL,
    [PrincipalId] int NOT NULL,
    [PrincipalDetailId] int NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NULL,
    [SubTotal] decimal(18,2) NOT NULL,
    [Status] nvarchar(20) NULL,
    [AdminComment] nvarchar(255) NULL,
    [VendorComment] nvarchar(255) NULL,
    [Comment] nvarchar(255) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Exchange] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Exchange_PrincipalDetail_PrincipalDetailId] FOREIGN KEY ([PrincipalDetailId]) REFERENCES [Account].[PrincipalDetail] ([Id]),
    CONSTRAINT [FK_Exchange_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Exchange_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Exchange_PurchaseQuote_PurchaseQuoteId] FOREIGN KEY ([PurchaseQuoteId]) REFERENCES [Transaction].[PurchaseQuote] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Exchange_Store_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Account].[Store] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transaction].[ProductReview] (
    [Id] uniqueidentifier NOT NULL,
    [Code] nvarchar(20) NULL,
    [ProductId] int NOT NULL,
    [PrincipalId] int NOT NULL,
    [PurchaseQuoteId] uniqueidentifier NOT NULL,
    [Rating] int NULL,
    [Comment] nvarchar(255) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_ProductReview] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductReview_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductReview_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductReview_PurchaseQuote_PurchaseQuoteId] FOREIGN KEY ([PurchaseQuoteId]) REFERENCES [Transaction].[PurchaseQuote] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Transaction].[Refund] (
    [Id] uniqueidentifier NOT NULL,
    [Number] nvarchar(100) NULL,
    [PurchaseQuoteId] uniqueidentifier NOT NULL,
    [ProductId] int NOT NULL,
    [StoreId] int NOT NULL,
    [PrincipalId] int NOT NULL,
    [PrincipalDetailId] int NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NULL,
    [SubTotal] decimal(18,2) NOT NULL,
    [Status] nvarchar(20) NULL,
    [AdminComment] nvarchar(255) NULL,
    [VendorComment] nvarchar(255) NULL,
    [Comment] nvarchar(255) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Refund] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Refund_PrincipalDetail_PrincipalDetailId] FOREIGN KEY ([PrincipalDetailId]) REFERENCES [Account].[PrincipalDetail] ([Id]),
    CONSTRAINT [FK_Refund_Principal_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Account].[Principal] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Refund_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Refund_PurchaseQuote_PurchaseQuoteId] FOREIGN KEY ([PurchaseQuoteId]) REFERENCES [Transaction].[PurchaseQuote] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Refund_Store_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Account].[Store] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transaction].[ExchangeImage] (
    [ExchangeId] uniqueidentifier NOT NULL,
    [ImageId] bigint NOT NULL,
    CONSTRAINT [PK_ExchangeImage] PRIMARY KEY ([ExchangeId], [ImageId]),
    CONSTRAINT [FK_ExchangeImage_Exchange_ExchangeId] FOREIGN KEY ([ExchangeId]) REFERENCES [Transaction].[Exchange] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ExchangeImage_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transaction].[ProductReviewImage] (
    [ProductReviewId] uniqueidentifier NOT NULL,
    [ImageId] bigint NOT NULL,
    CONSTRAINT [PK_ProductReviewImage] PRIMARY KEY ([ImageId], [ProductReviewId]),
    CONSTRAINT [FK_ProductReviewImage_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductReviewImage_ProductReview_ProductReviewId] FOREIGN KEY ([ProductReviewId]) REFERENCES [Transaction].[ProductReview] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transaction].[RefundImage] (
    [RefundId] uniqueidentifier NOT NULL,
    [ImageId] bigint NOT NULL,
    CONSTRAINT [PK_RefundImage] PRIMARY KEY ([ImageId], [RefundId]),
    CONSTRAINT [FK_RefundImage_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RefundImage_Refund_RefundId] FOREIGN KEY ([RefundId]) REFERENCES [Transaction].[Refund] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Attribute_BrandId] ON [Product].[Attribute] ([BrandId]);
GO

CREATE INDEX [IX_Attribute_CategoryId] ON [Product].[Attribute] ([CategoryId]);
GO

CREATE INDEX [IX_Banner_ImageId] ON [Common].[Banner] ([ImageId]);
GO

CREATE INDEX [IX_Blog_ImageId] ON [Common].[Blog] ([ImageId]);
GO

CREATE INDEX [IX_Brand_BwImageId] ON [Product].[Brand] ([BwImageId]);
GO

CREATE INDEX [IX_Brand_ImageId] ON [Product].[Brand] ([ImageId]);
GO

CREATE INDEX [IX_BrandCategory_CategoryId] ON [Product].[BrandCategory] ([CategoryId]);
GO

CREATE UNIQUE INDEX [IX_Cart_PrincipalId_ProductId_Price] ON [Transaction].[Cart] ([PrincipalId], [ProductId], [Price]);
GO

CREATE INDEX [IX_Cart_ProductId] ON [Transaction].[Cart] ([ProductId]);
GO

CREATE INDEX [IX_CartSaleItem_CartId] ON [Transaction].[CartSaleItem] ([CartId]);
GO

CREATE INDEX [IX_CartSaleItem_ProductId] ON [Transaction].[CartSaleItem] ([ProductId]);
GO

CREATE INDEX [IX_CartSaleItem_SaleItemId] ON [Transaction].[CartSaleItem] ([SaleItemId]);
GO

CREATE INDEX [IX_CartSaleItem_StoreId] ON [Transaction].[CartSaleItem] ([StoreId]);
GO

CREATE INDEX [IX_Catalogue_DocumentId] ON [Common].[Catalogue] ([DocumentId]);
GO

CREATE INDEX [IX_Catalogue_ImageId] ON [Common].[Catalogue] ([ImageId]);
GO

CREATE INDEX [IX_Category_ImageId] ON [Product].[Category] ([ImageId]);
GO

CREATE INDEX [IX_Category_ParentId] ON [Product].[Category] ([ParentId]);
GO

CREATE INDEX [IX_Certificate_ImageId] ON [Common].[Certificate] ([ImageId]);
GO

CREATE INDEX [IX_CompanyMember_PrincipalId] ON [Account].[CompanyMember] ([PrincipalId]);
GO

CREATE INDEX [IX_Country_ImageId] ON [Common].[Country] ([ImageId]);
GO

CREATE UNIQUE INDEX [IX_Country_PhoneCode] ON [Common].[Country] ([PhoneCode]);
GO

CREATE UNIQUE INDEX [IX_Currency_Code] ON [Common].[Currency] ([Code]);
GO

CREATE INDEX [IX_CustomerOrder_AddressId] ON [Transaction].[CustomerOrder] ([AddressId]);
GO

CREATE INDEX [IX_CustomerOrder_HiglightedProductId] ON [Transaction].[CustomerOrder] ([HiglightedProductId]);
GO

CREATE INDEX [IX_CustomerOrder_PaymentMethodId] ON [Transaction].[CustomerOrder] ([PaymentMethodId]);
GO

CREATE INDEX [IX_CustomerOrder_PrincipalDetailId] ON [Transaction].[CustomerOrder] ([PrincipalDetailId]);
GO

CREATE INDEX [IX_CustomerOrder_PrincipalId] ON [Transaction].[CustomerOrder] ([PrincipalId]);
GO

CREATE INDEX [IX_CustomerOrderItem_CustomerOrderId] ON [Transaction].[CustomerOrderItem] ([CustomerOrderId]);
GO

CREATE INDEX [IX_CustomerOrderItem_ProductId] ON [Transaction].[CustomerOrderItem] ([ProductId]);
GO

CREATE INDEX [IX_Department_PrincipalDetailId] ON [Account].[Department] ([PrincipalDetailId]);
GO

CREATE UNIQUE INDEX [IX_Document_Name] ON [Common].[Document] ([Name]);
GO

CREATE INDEX [IX_Exchange_PrincipalDetailId] ON [Transaction].[Exchange] ([PrincipalDetailId]);
GO

CREATE INDEX [IX_Exchange_PrincipalId] ON [Transaction].[Exchange] ([PrincipalId]);
GO

CREATE INDEX [IX_Exchange_ProductId] ON [Transaction].[Exchange] ([ProductId]);
GO

CREATE INDEX [IX_Exchange_PurchaseQuoteId] ON [Transaction].[Exchange] ([PurchaseQuoteId]);
GO

CREATE INDEX [IX_Exchange_StoreId] ON [Transaction].[Exchange] ([StoreId]);
GO

CREATE INDEX [IX_ExchangeImage_ImageId] ON [Transaction].[ExchangeImage] ([ImageId]);
GO

CREATE INDEX [IX_Faq_FaqGroupId] ON [Common].[Faq] ([FaqGroupId]);
GO

CREATE UNIQUE INDEX [IX_Image_Name] ON [Common].[Image] ([Name]);
GO

CREATE INDEX [IX_Inquiry_PrincipalId] ON [Transaction].[Inquiry] ([PrincipalId]);
GO

CREATE INDEX [IX_Inquiry_ProductId] ON [Transaction].[Inquiry] ([ProductId]);
GO

CREATE INDEX [IX_Job_JobFieldId] ON [Common].[Job] ([JobFieldId]);
GO

CREATE INDEX [IX_JobApplicant_DocumentId] ON [Common].[JobApplicant] ([DocumentId]);
GO

CREATE INDEX [IX_JobApplicant_JobId] ON [Common].[JobApplicant] ([JobId]);
GO

CREATE UNIQUE INDEX [IX_Language_Code] ON [Common].[Language] ([Code]) WHERE [Code] IS NOT NULL;
GO

CREATE INDEX [IX_Language_ImageId] ON [Common].[Language] ([ImageId]);
GO

CREATE INDEX [IX_Notification_ImageId] ON [Account].[Notification] ([ImageId]);
GO

CREATE INDEX [IX_Notification_PrincipalId] ON [Account].[Notification] ([PrincipalId]);
GO

CREATE INDEX [IX_OneTimeToken_PrincipalId] ON [Security].[OneTimeToken] ([PrincipalId]);
GO

CREATE UNIQUE INDEX [IX_OneTimeToken_Token] ON [Security].[OneTimeToken] ([Token]);
GO

CREATE UNIQUE INDEX [IX_OneTimeToken_Type_PrincipalId] ON [Security].[OneTimeToken] ([Type], [PrincipalId]);
GO

CREATE UNIQUE INDEX [IX_Otp_Code] ON [Security].[Otp] ([Code]);
GO

CREATE INDEX [IX_Otp_PrincipalId] ON [Security].[Otp] ([PrincipalId]);
GO

CREATE INDEX [IX_PaymentMethod_ImageArId] ON [Transaction].[PaymentMethod] ([ImageArId]);
GO

CREATE INDEX [IX_PaymentMethod_ImageId] ON [Transaction].[PaymentMethod] ([ImageId]);
GO

CREATE UNIQUE INDEX [IX_Principal_CountryCode_PhoneNumber] ON [Account].[Principal] ([CountryCode], [PhoneNumber]) WHERE [CountryCode] IS NOT NULL AND [PhoneNumber] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Principal_Email] ON [Account].[Principal] ([Email]);
GO

CREATE INDEX [IX_Principal_ImageId] ON [Account].[Principal] ([ImageId]);
GO

CREATE UNIQUE INDEX [IX_Principal_Username] ON [Account].[Principal] ([Username]);
GO

CREATE UNIQUE INDEX [IX_PrincipalAddress_PrincipalId_Name] ON [Account].[PrincipalAddress] ([PrincipalId], [Name]);
GO

CREATE INDEX [IX_PrincipalBank_CurrencyId] ON [Account].[PrincipalBank] ([CurrencyId]);
GO

CREATE INDEX [IX_PrincipalBank_DocumentId] ON [Account].[PrincipalBank] ([DocumentId]);
GO

CREATE UNIQUE INDEX [IX_PrincipalBank_Iban] ON [Account].[PrincipalBank] ([Iban]);
GO

CREATE INDEX [IX_PrincipalBank_PrincipalId] ON [Account].[PrincipalBank] ([PrincipalId]);
GO

CREATE INDEX [IX_PrincipalDepartment_PrincipalId] ON [Account].[PrincipalDepartment] ([PrincipalId]);
GO

CREATE INDEX [IX_PrincipalDetail_CountryId] ON [Account].[PrincipalDetail] ([CountryId]);
GO

CREATE UNIQUE INDEX [IX_PrincipalDetail_PrincipalId] ON [Account].[PrincipalDetail] ([PrincipalId]);
GO

CREATE INDEX [IX_PrincipalDetailItem_PrincipalDetailId] ON [Account].[PrincipalDetailItem] ([PrincipalDetailId]);
GO

CREATE INDEX [IX_PrincipalRole_RoleId] ON [Account].[PrincipalRole] ([RoleId]);
GO

CREATE INDEX [IX_PrincipalStore_StoreId] ON [Account].[PrincipalStore] ([StoreId]);
GO

CREATE INDEX [IX_PrincipalWallet_PrincipalId] ON [Account].[PrincipalWallet] ([PrincipalId]);
GO

CREATE UNIQUE INDEX [IX_Privilege_Name] ON [Account].[Privilege] ([Name]);
GO

CREATE INDEX [IX_Product_BrandId] ON [Product].[Product] ([BrandId]);
GO

CREATE INDEX [IX_Product_IconId] ON [Product].[Product] ([IconId]);
GO

CREATE INDEX [IX_Product_ImageId] ON [Product].[Product] ([ImageId]);
GO

CREATE INDEX [IX_Product_PrincipalDetailId] ON [Product].[Product] ([PrincipalDetailId]);
GO

CREATE INDEX [IX_ProductCategory_ProductId] ON [Product].[ProductCategory] ([ProductId]);
GO

CREATE INDEX [IX_ProductImage_ImageId] ON [Product].[ProductImage] ([ImageId]);
GO

CREATE UNIQUE INDEX [IX_ProductImage_ProductId_ImageId] ON [Product].[ProductImage] ([ProductId], [ImageId]);
GO

CREATE UNIQUE INDEX [IX_ProductProperty_ProductId_SortOrder] ON [Product].[ProductProperty] ([ProductId], [SortOrder]);
GO

CREATE INDEX [IX_ProductPropertyItem_ImageId] ON [Product].[ProductPropertyItem] ([ImageId]);
GO

CREATE INDEX [IX_ProductPropertyItem_PropertyId] ON [Product].[ProductPropertyItem] ([PropertyId]);
GO

CREATE INDEX [IX_ProductReview_PrincipalId] ON [Transaction].[ProductReview] ([PrincipalId]);
GO

CREATE INDEX [IX_ProductReview_ProductId] ON [Transaction].[ProductReview] ([ProductId]);
GO

CREATE INDEX [IX_ProductReview_PurchaseQuoteId] ON [Transaction].[ProductReview] ([PurchaseQuoteId]);
GO

CREATE INDEX [IX_ProductReviewImage_ProductReviewId] ON [Transaction].[ProductReviewImage] ([ProductReviewId]);
GO

CREATE INDEX [IX_PurchaseQuote_CustomerOrderId] ON [Transaction].[PurchaseQuote] ([CustomerOrderId]);
GO

CREATE INDEX [IX_PurchaseQuote_CustomerOrderItemId] ON [Transaction].[PurchaseQuote] ([CustomerOrderItemId]);
GO

CREATE INDEX [IX_PurchaseQuote_ProductId] ON [Transaction].[PurchaseQuote] ([ProductId]);
GO

CREATE INDEX [IX_PurchaseQuote_StoreId] ON [Transaction].[PurchaseQuote] ([StoreId]);
GO

CREATE INDEX [IX_PurchaseQuote_StoreOrderId] ON [Transaction].[PurchaseQuote] ([StoreOrderId]);
GO

CREATE UNIQUE INDEX [IX_RefreshToken_PrincipalId] ON [Security].[RefreshToken] ([PrincipalId]);
GO

CREATE UNIQUE INDEX [IX_RefreshToken_Token] ON [Security].[RefreshToken] ([Token]);
GO

CREATE INDEX [IX_Refund_PrincipalDetailId] ON [Transaction].[Refund] ([PrincipalDetailId]);
GO

CREATE INDEX [IX_Refund_PrincipalId] ON [Transaction].[Refund] ([PrincipalId]);
GO

CREATE INDEX [IX_Refund_ProductId] ON [Transaction].[Refund] ([ProductId]);
GO

CREATE INDEX [IX_Refund_PurchaseQuoteId] ON [Transaction].[Refund] ([PurchaseQuoteId]);
GO

CREATE INDEX [IX_Refund_StoreId] ON [Transaction].[Refund] ([StoreId]);
GO

CREATE INDEX [IX_RefundImage_RefundId] ON [Transaction].[RefundImage] ([RefundId]);
GO

CREATE UNIQUE INDEX [IX_Role_Name] ON [Account].[Role] ([Name]);
GO

CREATE INDEX [IX_RolePrivilege_RoleId] ON [Account].[RolePrivilege] ([RoleId]);
GO

CREATE UNIQUE INDEX [IX_SaleItem_ProductId_StoreId] ON [Product].[SaleItem] ([ProductId], [StoreId]);
GO

CREATE INDEX [IX_SaleItem_StoreId] ON [Product].[SaleItem] ([StoreId]);
GO

CREATE INDEX [IX_Shipping_CustomerOrderId] ON [Transaction].[Shipping] ([CustomerOrderId]);
GO

CREATE INDEX [IX_Shipping_ShippingMethodId] ON [Transaction].[Shipping] ([ShippingMethodId]);
GO

CREATE INDEX [IX_Store_PrincipalId] ON [Account].[Store] ([PrincipalId]);
GO

CREATE INDEX [IX_StoreOrder_CustomerOrderId] ON [Transaction].[StoreOrder] ([CustomerOrderId]);
GO

CREATE UNIQUE INDEX [IX_StoreOrder_InvoiceNumber] ON [Transaction].[StoreOrder] ([InvoiceNumber]);
GO

CREATE INDEX [IX_StoreOrder_StoreId] ON [Transaction].[StoreOrder] ([StoreId]);
GO

CREATE INDEX [IX_WebPage_ImageId] ON [Common].[WebPage] ([ImageId]);
GO

CREATE UNIQUE INDEX [IX_WebPage_Slug] ON [Common].[WebPage] ([Slug]);
GO

CREATE INDEX [IX_WebsiteProfile_FaviconId] ON [Common].[WebsiteProfile] ([FaviconId]);
GO

CREATE INDEX [IX_WebsiteProfile_ImageId] ON [Common].[WebsiteProfile] ([ImageId]);
GO

CREATE INDEX [IX_WishList_PrincipalId] ON [Transaction].[WishList] ([PrincipalId]);
GO

CREATE UNIQUE INDEX [IX_WishList_ProductId_PrincipalId] ON [Transaction].[WishList] ([ProductId], [PrincipalId]);
GO

CREATE INDEX [IX_Withdraw_ImageId] ON [Transaction].[Withdraw] ([ImageId]);
GO

CREATE INDEX [IX_Withdraw_PrincipalId] ON [Transaction].[Withdraw] ([PrincipalId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240523091250_Init', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product].[Brand]') AND [c].[name] = N'NameEn');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Product].[Brand] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Product].[Brand] ALTER COLUMN [NameEn] nvarchar(100) NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Product_Slug] ON [Product].[Product] ([Slug]) WHERE [Slug] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Category_Slug] ON [Product].[Category] ([Slug]);
GO

CREATE UNIQUE INDEX [IX_Brand_NameEn] ON [Product].[Brand] ([NameEn]);
GO

CREATE UNIQUE INDEX [IX_Brand_Slug] ON [Product].[Brand] ([Slug]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240603115547_AddIndexes', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Product].[Attribute] DROP CONSTRAINT [FK_Attribute_Brand_BrandId];
GO

ALTER TABLE [Product].[Attribute] DROP CONSTRAINT [FK_Attribute_Category_CategoryId];
GO

DROP TABLE [Product].[ProductPropertyItem];
GO

DROP TABLE [Product].[ProductProperty];
GO

DROP INDEX [IX_Attribute_BrandId] ON [Product].[Attribute];
GO

DROP INDEX [IX_Attribute_CategoryId] ON [Product].[Attribute];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product].[Attribute]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Product].[Attribute] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Product].[Attribute] DROP COLUMN [Name];
GO

EXEC sp_rename N'[Product].[Attribute].[Options]', N'GroupEn', N'COLUMN';
GO

EXEC sp_rename N'[Product].[Attribute].[CategoryId]', N'SortOrder', N'COLUMN';
GO

EXEC sp_rename N'[Product].[Attribute].[BrandId]', N'GroupOrder', N'COLUMN';
GO

ALTER TABLE [Product].[Attribute] ADD [Code] nvarchar(100) NULL;
GO

ALTER TABLE [Product].[Attribute] ADD [GroupAr] nvarchar(max) NULL;
GO

ALTER TABLE [Product].[Attribute] ADD [NameAr] nvarchar(max) NULL;
GO

ALTER TABLE [Product].[Attribute] ADD [NameEn] nvarchar(450) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Product].[Attribute] ADD [Unit1Ar] nvarchar(max) NULL;
GO

ALTER TABLE [Product].[Attribute] ADD [Unit1En] nvarchar(max) NULL;
GO

ALTER TABLE [Product].[Attribute] ADD [Unit2Ar] nvarchar(max) NULL;
GO

ALTER TABLE [Product].[Attribute] ADD [Unit2En] nvarchar(max) NULL;
GO

ALTER TABLE [Product].[Attribute] ADD [Unit3Ar] nvarchar(max) NULL;
GO

ALTER TABLE [Product].[Attribute] ADD [Unit3En] nvarchar(max) NULL;
GO

CREATE TABLE [Product].[Property] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(100) NULL,
    [NameEn] nvarchar(450) NOT NULL,
    [NameAr] nvarchar(max) NULL,
    [FieldsEn] nvarchar(max) NOT NULL,
    [FieldsAr] nvarchar(max) NOT NULL,
    [Type] nvarchar(10) NOT NULL,
    [SortOrder] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_Property] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Product].[ProductAttribute] (
    [Id] uniqueidentifier NOT NULL,
    [ProductId] int NOT NULL,
    [PropertyId] int NOT NULL,
    [AttributeId] int NOT NULL,
    [Value1En] nvarchar(max) NOT NULL,
    [Value1Ar] nvarchar(max) NULL,
    [Value2En] nvarchar(max) NULL,
    [Value2Ar] nvarchar(max) NULL,
    [Value3En] nvarchar(max) NULL,
    [Value3Ar] nvarchar(max) NULL,
    [ImageId] bigint NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_ProductAttribute] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductAttribute_Attribute_AttributeId] FOREIGN KEY ([AttributeId]) REFERENCES [Product].[Attribute] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductAttribute_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Common].[Image] ([Id]),
    CONSTRAINT [FK_ProductAttribute_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductAttribute_Property_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Product].[Property] ([Id]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [IX_Attribute_NameEn] ON [Product].[Attribute] ([NameEn]);
GO

CREATE INDEX [IX_ProductAttribute_AttributeId] ON [Product].[ProductAttribute] ([AttributeId]);
GO

CREATE INDEX [IX_ProductAttribute_ImageId] ON [Product].[ProductAttribute] ([ImageId]);
GO

CREATE UNIQUE INDEX [IX_ProductAttribute_ProductId_AttributeId] ON [Product].[ProductAttribute] ([ProductId], [AttributeId]);
GO

CREATE INDEX [IX_ProductAttribute_PropertyId] ON [Product].[ProductAttribute] ([PropertyId]);
GO

CREATE UNIQUE INDEX [IX_Property_NameEn] ON [Product].[Property] ([NameEn]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240605085654_RefactorAttribute', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Transaction].[StoreOrder] ADD [PurchaseQuoteStatus] nvarchar(20) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240606082625_StoreOrderPurchaseQuoteStatus', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_Attribute_NameEn] ON [Product].[Attribute];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product].[Attribute]') AND [c].[name] = N'Unit3En');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Product].[Attribute] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Product].[Attribute] ALTER COLUMN [Unit3En] nvarchar(450) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product].[Attribute]') AND [c].[name] = N'Unit2En');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Product].[Attribute] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Product].[Attribute] ALTER COLUMN [Unit2En] nvarchar(450) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product].[Attribute]') AND [c].[name] = N'Unit1En');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Product].[Attribute] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Product].[Attribute] ALTER COLUMN [Unit1En] nvarchar(450) NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product].[Attribute]') AND [c].[name] = N'GroupEn');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Product].[Attribute] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Product].[Attribute] ALTER COLUMN [GroupEn] nvarchar(450) NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Attribute_GroupEn_NameEn_Unit1En_Unit2En_Unit3En] ON [Product].[Attribute] ([GroupEn], [NameEn], [Unit1En], [Unit2En], [Unit3En]) WHERE [Unit1En] IS NOT NULL AND [Unit2En] IS NOT NULL AND [Unit3En] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240609054309_UpdateAttributeIndex', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Transaction].[StoreOrder].[OtoId]', N'DeliveryId', N'COLUMN';
GO

EXEC sp_rename N'[Transaction].[CustomerOrder].[UrwayTransactionId]', N'PaymentTransactionId', N'COLUMN';
GO

EXEC sp_rename N'[Transaction].[CustomerOrder].[UrwayTrackId]', N'PaymentTrackId', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240609060744_ChangeVendorSpecificFieldNames', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction].[CustomerOrder]') AND [c].[name] = N'PurchaseQuoteStatus');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Transaction].[CustomerOrder] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Transaction].[CustomerOrder] DROP COLUMN [PurchaseQuoteStatus];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240609105414_RemovePurchaseQuoteStatusFromCustomerOrder', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Account].[Store] ADD [Type] nvarchar(20) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240610071502_AddStoreType', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Transaction].[Withdraw] ADD [AccountNumber] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Transaction].[Withdraw] ADD [BankHolder] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Transaction].[Withdraw] ADD [BankName] nvarchar(100) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Transaction].[Withdraw] ADD [WithdrawMethod] nvarchar(50) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240629185606_AddWithdrawalModels', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240630054838_UpdateWithdrawal', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Logging].[ErrorLog];
GO

DROP TABLE [Logging].[InternalRequestLog];
GO

DROP TABLE [Logging].[InternalResponseLog];
GO

DROP TABLE [Logging].[RequestLog];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240709115422_RemoveLogging', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_Image_Name] ON [Common].[Image];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Common].[Image]') AND [c].[name] = N'Url');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Common].[Image] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Common].[Image] DROP COLUMN [Url];
GO

ALTER TABLE [Common].[Image] ADD [Group] nvarchar(20) NULL;
GO

CREATE UNIQUE INDEX [IX_Image_Group_Name] ON [Common].[Image] ([Group], [Name]) WHERE [Group] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240722140205_RefactorImage', N'8.0.0');
GO

COMMIT;
GO

