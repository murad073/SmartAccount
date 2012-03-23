
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/12/2012 00:09:01
-- Generated from EDMX file: C:\Murad Boss\My Jobs\Running Projects\Codeplex SmartAccount\Project Source\trunk\DALs\SQLExpress\SQLExpress\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SmartAccount];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0C85DE4D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Membership] DROP CONSTRAINT [FK__aspnet_Me__Appli__0C85DE4D];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Me__UserI__0D7A0286]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Membership] DROP CONSTRAINT [FK__aspnet_Me__UserI__0D7A0286];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pa__Appli__45BE5BA9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Paths] DROP CONSTRAINT [FK__aspnet_Pa__Appli__45BE5BA9];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__4D5F7D71]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers] DROP CONSTRAINT [FK__aspnet_Pe__PathI__4D5F7D71];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__531856C7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] DROP CONSTRAINT [FK__aspnet_Pe__PathI__531856C7];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pe__UserI__540C7B00]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] DROP CONSTRAINT [FK__aspnet_Pe__UserI__540C7B00];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pr__UserI__236943A5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Profile] DROP CONSTRAINT [FK__aspnet_Pr__UserI__236943A5];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2EDAF651]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Roles] DROP CONSTRAINT [FK__aspnet_Ro__Appli__2EDAF651];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Us__Appli__787EE5A0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Users] DROP CONSTRAINT [FK__aspnet_Us__Appli__787EE5A0];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3587F3E0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_UsersInRoles] DROP CONSTRAINT [FK__aspnet_Us__RoleI__3587F3E0];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Us__UserI__3493CFA7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_UsersInRoles] DROP CONSTRAINT [FK__aspnet_Us__UserI__3493CFA7];
GO
IF OBJECT_ID(N'[dbo].[FK_BankRecord_Record]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BankRecord] DROP CONSTRAINT [FK_BankRecord_Record];
GO
IF OBJECT_ID(N'[dbo].[FK_FixedAsset_Record]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FixedAsset] DROP CONSTRAINT [FK_FixedAsset_Record];
GO
IF OBJECT_ID(N'[dbo].[FK_OpeningBalance_ProjectCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OpeningBalance] DROP CONSTRAINT [FK_OpeningBalance_ProjectCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectCategory_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectCategory] DROP CONSTRAINT [FK_ProjectCategory_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectCategory_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectCategory] DROP CONSTRAINT [FK_ProjectCategory_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_Record_ProjectCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_Record_ProjectCategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[aspnet_Applications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Applications];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Membership]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Membership];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Paths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Paths];
GO
IF OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_PersonalizationAllUsers];
GO
IF OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_PersonalizationPerUser];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Profile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Profile];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Roles];
GO
IF OBJECT_ID(N'[dbo].[aspnet_SchemaVersions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_SchemaVersions];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Users];
GO
IF OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_UsersInRoles];
GO
IF OBJECT_ID(N'[dbo].[aspnet_WebEvent_Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_WebEvent_Events];
GO
IF OBJECT_ID(N'[dbo].[BankRecord]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BankRecord];
GO
IF OBJECT_ID(N'[dbo].[CashRecord]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashRecord];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[CategoryType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryType];
GO
IF OBJECT_ID(N'[dbo].[FixedAsset]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FixedAsset];
GO
IF OBJECT_ID(N'[dbo].[LedgerRecord]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LedgerRecord];
GO
IF OBJECT_ID(N'[dbo].[Log]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Log];
GO
IF OBJECT_ID(N'[dbo].[LogType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogType];
GO
IF OBJECT_ID(N'[dbo].[OpeningBalance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OpeningBalance];
GO
IF OBJECT_ID(N'[dbo].[Parameter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parameter];
GO
IF OBJECT_ID(N'[dbo].[Project]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Project];
GO
IF OBJECT_ID(N'[dbo].[ProjectCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectCategory];
GO
IF OBJECT_ID(N'[dbo].[Record]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Record];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Type];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[CapitalCategory]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[CapitalCategory];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[FixedAssetCategory]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[FixedAssetCategory];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[RevenueCategory]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[RevenueCategory];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[RevenueRecord]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[RevenueRecord];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[vw_aspnet_Applications]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[vw_aspnet_Applications];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[vw_aspnet_MembershipUsers]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[vw_aspnet_MembershipUsers];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[vw_aspnet_Profiles]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[vw_aspnet_Profiles];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[vw_aspnet_Roles]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[vw_aspnet_Roles];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[vw_aspnet_Users]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[vw_aspnet_Users];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[vw_aspnet_UsersInRoles]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[vw_aspnet_UsersInRoles];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[vw_aspnet_WebPartState_Paths]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[vw_aspnet_WebPartState_Paths];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[vw_aspnet_WebPartState_Shared]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[vw_aspnet_WebPartState_Shared];
GO
IF OBJECT_ID(N'[SmartAccountModelStoreContainer].[vw_aspnet_WebPartState_User]', 'U') IS NOT NULL
    DROP TABLE [SmartAccountModelStoreContainer].[vw_aspnet_WebPartState_User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'aspnet_Applications'
CREATE TABLE [dbo].[aspnet_Applications] (
    [ApplicationName] nvarchar(256)  NOT NULL,
    [LoweredApplicationName] nvarchar(256)  NOT NULL,
    [ApplicationId] uniqueidentifier  NOT NULL,
    [Description] nvarchar(256)  NULL
);
GO

-- Creating table 'aspnet_Membership'
CREATE TABLE [dbo].[aspnet_Membership] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [Password] nvarchar(128)  NOT NULL,
    [PasswordFormat] int  NOT NULL,
    [PasswordSalt] nvarchar(128)  NOT NULL,
    [MobilePIN] nvarchar(16)  NULL,
    [Email] nvarchar(256)  NULL,
    [LoweredEmail] nvarchar(256)  NULL,
    [PasswordQuestion] nvarchar(256)  NULL,
    [PasswordAnswer] nvarchar(128)  NULL,
    [IsApproved] bit  NOT NULL,
    [IsLockedOut] bit  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [LastLoginDate] datetime  NOT NULL,
    [LastPasswordChangedDate] datetime  NOT NULL,
    [LastLockoutDate] datetime  NOT NULL,
    [FailedPasswordAttemptCount] int  NOT NULL,
    [FailedPasswordAttemptWindowStart] datetime  NOT NULL,
    [FailedPasswordAnswerAttemptCount] int  NOT NULL,
    [FailedPasswordAnswerAttemptWindowStart] datetime  NOT NULL,
    [Comment] nvarchar(max)  NULL
);
GO

-- Creating table 'aspnet_Paths'
CREATE TABLE [dbo].[aspnet_Paths] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [PathId] uniqueidentifier  NOT NULL,
    [Path] nvarchar(256)  NOT NULL,
    [LoweredPath] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'aspnet_PersonalizationAllUsers'
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers] (
    [PathId] uniqueidentifier  NOT NULL,
    [PageSettings] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_PersonalizationPerUser'
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser] (
    [Id] uniqueidentifier  NOT NULL,
    [PathId] uniqueidentifier  NULL,
    [UserId] uniqueidentifier  NULL,
    [PageSettings] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_Profile'
CREATE TABLE [dbo].[aspnet_Profile] (
    [UserId] uniqueidentifier  NOT NULL,
    [PropertyNames] nvarchar(max)  NOT NULL,
    [PropertyValuesString] nvarchar(max)  NOT NULL,
    [PropertyValuesBinary] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_Roles'
CREATE TABLE [dbo].[aspnet_Roles] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [RoleId] uniqueidentifier  NOT NULL,
    [RoleName] nvarchar(256)  NOT NULL,
    [LoweredRoleName] nvarchar(256)  NOT NULL,
    [Description] nvarchar(256)  NULL
);
GO

-- Creating table 'aspnet_SchemaVersions'
CREATE TABLE [dbo].[aspnet_SchemaVersions] (
    [Feature] nvarchar(128)  NOT NULL,
    [CompatibleSchemaVersion] nvarchar(128)  NOT NULL,
    [IsCurrentVersion] bit  NOT NULL
);
GO

-- Creating table 'aspnet_Users'
CREATE TABLE [dbo].[aspnet_Users] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [LoweredUserName] nvarchar(256)  NOT NULL,
    [MobileAlias] nvarchar(16)  NULL,
    [IsAnonymous] bit  NOT NULL,
    [LastActivityDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_WebEvent_Events'
CREATE TABLE [dbo].[aspnet_WebEvent_Events] (
    [EventId] char(32)  NOT NULL,
    [EventTimeUtc] datetime  NOT NULL,
    [EventTime] datetime  NOT NULL,
    [EventType] nvarchar(256)  NOT NULL,
    [EventSequence] decimal(19,0)  NOT NULL,
    [EventOccurrence] decimal(19,0)  NOT NULL,
    [EventCode] int  NOT NULL,
    [EventDetailCode] int  NOT NULL,
    [Message] nvarchar(1024)  NULL,
    [ApplicationPath] nvarchar(256)  NULL,
    [ApplicationVirtualPath] nvarchar(256)  NULL,
    [MachineName] nvarchar(256)  NOT NULL,
    [RequestUrl] nvarchar(1024)  NULL,
    [ExceptionType] nvarchar(256)  NULL,
    [Details] nvarchar(max)  NULL
);
GO

-- Creating table 'BankRecords'
CREATE TABLE [dbo].[BankRecords] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecordID] int  NOT NULL,
    [ChequeNo] varchar(500)  NULL
);
GO

-- Creating table 'CashRecords'
CREATE TABLE [dbo].[CashRecords] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecordID] int  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ParentID] int  NULL,
    [Name] varchar(500)  NOT NULL,
    [IsActive] bit  NULL,
    [Type] varchar(50)  NULL
);
GO

-- Creating table 'CategoryTypes'
CREATE TABLE [dbo].[CategoryTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CategoryID] int  NOT NULL,
    [Type] varchar(50)  NOT NULL
);
GO

-- Creating table 'FixedAssets'
CREATE TABLE [dbo].[FixedAssets] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(500)  NULL,
    [DepreciationRate] int  NULL,
    [DepreciatedValue] int  NULL,
    [DepreciationType] varchar(50)  NULL,
    [IsActive] bit  NULL,
    [ByForceDisposed] bit  NULL,
    [RecordID] int  NULL
);
GO

-- Creating table 'LedgerRecords'
CREATE TABLE [dbo].[LedgerRecords] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecordID] int  NOT NULL
);
GO

-- Creating table 'Logs'
CREATE TABLE [dbo].[Logs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [UserName] varchar(500)  NULL,
    [Type] varchar(50)  NOT NULL,
    [Message] varchar(max)  NULL
);
GO

-- Creating table 'LogTypes'
CREATE TABLE [dbo].[LogTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [DefaultMessage] varchar(max)  NULL
);
GO

-- Creating table 'OpeningBalances'
CREATE TABLE [dbo].[OpeningBalances] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [ProjectCategoryID] int  NULL,
    [Balance] int  NULL,
    [IsActive] bit  NULL,
    [Description] varchar(max)  NULL
);
GO

-- Creating table 'Parameters'
CREATE TABLE [dbo].[Parameters] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Key] varchar(500)  NOT NULL,
    [Value] varchar(max)  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [IsActive] bit  NULL,
    [Name] varchar(500)  NOT NULL
);
GO

-- Creating table 'ProjectCategories'
CREATE TABLE [dbo].[ProjectCategories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProjectID] int  NULL,
    [CategoryID] int  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'Records'
CREATE TABLE [dbo].[Records] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProjectCategoryID] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [VoucherNo] varchar(500)  NOT NULL,
    [Debit] float  NULL,
    [Credit] float  NULL,
    [Description] varchar(max)  NULL,
    [IsActive] bit  NULL,
    [Type] varchar(50)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Types'
CREATE TABLE [dbo].[Types] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(500)  NULL
);
GO

-- Creating table 'CapitalCategories'
CREATE TABLE [dbo].[CapitalCategories] (
    [CategoryID] int  NOT NULL,
    [CategoryName] varchar(500)  NOT NULL,
    [Value] int  NULL
);
GO

-- Creating table 'FixedAssetCategories'
CREATE TABLE [dbo].[FixedAssetCategories] (
    [CategoryID] int  NOT NULL,
    [CategoryName] varchar(500)  NOT NULL,
    [Value] int  NULL
);
GO

-- Creating table 'RevenueCategories'
CREATE TABLE [dbo].[RevenueCategories] (
    [CategoryID] int  NOT NULL,
    [CategoryName] varchar(500)  NOT NULL,
    [Value] int  NULL
);
GO

-- Creating table 'RevenueRecords'
CREATE TABLE [dbo].[RevenueRecords] (
    [Date] datetime  NOT NULL,
    [VoucherNo] varchar(500)  NOT NULL,
    [Credit] float  NULL,
    [Debit] float  NULL,
    [Note] varchar(max)  NULL
);
GO

-- Creating table 'vw_aspnet_Applications'
CREATE TABLE [dbo].[vw_aspnet_Applications] (
    [ApplicationName] nvarchar(256)  NOT NULL,
    [LoweredApplicationName] nvarchar(256)  NOT NULL,
    [ApplicationId] uniqueidentifier  NOT NULL,
    [Description] nvarchar(256)  NULL
);
GO

-- Creating table 'vw_aspnet_MembershipUsers'
CREATE TABLE [dbo].[vw_aspnet_MembershipUsers] (
    [UserId] uniqueidentifier  NOT NULL,
    [PasswordFormat] int  NOT NULL,
    [MobilePIN] nvarchar(16)  NULL,
    [Email] nvarchar(256)  NULL,
    [LoweredEmail] nvarchar(256)  NULL,
    [PasswordQuestion] nvarchar(256)  NULL,
    [PasswordAnswer] nvarchar(128)  NULL,
    [IsApproved] bit  NOT NULL,
    [IsLockedOut] bit  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [LastLoginDate] datetime  NOT NULL,
    [LastPasswordChangedDate] datetime  NOT NULL,
    [LastLockoutDate] datetime  NOT NULL,
    [FailedPasswordAttemptCount] int  NOT NULL,
    [FailedPasswordAttemptWindowStart] datetime  NOT NULL,
    [FailedPasswordAnswerAttemptCount] int  NOT NULL,
    [FailedPasswordAnswerAttemptWindowStart] datetime  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [ApplicationId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [MobileAlias] nvarchar(16)  NULL,
    [IsAnonymous] bit  NOT NULL,
    [LastActivityDate] datetime  NOT NULL
);
GO

-- Creating table 'vw_aspnet_Profiles'
CREATE TABLE [dbo].[vw_aspnet_Profiles] (
    [UserId] uniqueidentifier  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL,
    [DataSize] int  NULL
);
GO

-- Creating table 'vw_aspnet_Roles'
CREATE TABLE [dbo].[vw_aspnet_Roles] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [RoleId] uniqueidentifier  NOT NULL,
    [RoleName] nvarchar(256)  NOT NULL,
    [LoweredRoleName] nvarchar(256)  NOT NULL,
    [Description] nvarchar(256)  NULL
);
GO

-- Creating table 'vw_aspnet_Users'
CREATE TABLE [dbo].[vw_aspnet_Users] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [LoweredUserName] nvarchar(256)  NOT NULL,
    [MobileAlias] nvarchar(16)  NULL,
    [IsAnonymous] bit  NOT NULL,
    [LastActivityDate] datetime  NOT NULL
);
GO

-- Creating table 'vw_aspnet_UsersInRoles'
CREATE TABLE [dbo].[vw_aspnet_UsersInRoles] (
    [UserId] uniqueidentifier  NOT NULL,
    [RoleId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'vw_aspnet_WebPartState_Paths'
CREATE TABLE [dbo].[vw_aspnet_WebPartState_Paths] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [PathId] uniqueidentifier  NOT NULL,
    [Path] nvarchar(256)  NOT NULL,
    [LoweredPath] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'vw_aspnet_WebPartState_Shared'
CREATE TABLE [dbo].[vw_aspnet_WebPartState_Shared] (
    [PathId] uniqueidentifier  NOT NULL,
    [DataSize] int  NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'vw_aspnet_WebPartState_User'
CREATE TABLE [dbo].[vw_aspnet_WebPartState_User] (
    [PathId] uniqueidentifier  NULL,
    [UserId] uniqueidentifier  NULL,
    [DataSize] int  NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_UsersInRoles'
CREATE TABLE [dbo].[aspnet_UsersInRoles] (
    [aspnet_Roles_RoleId] uniqueidentifier  NOT NULL,
    [aspnet_Users_UserId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ApplicationId] in table 'aspnet_Applications'
ALTER TABLE [dbo].[aspnet_Applications]
ADD CONSTRAINT [PK_aspnet_Applications]
    PRIMARY KEY CLUSTERED ([ApplicationId] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Membership'
ALTER TABLE [dbo].[aspnet_Membership]
ADD CONSTRAINT [PK_aspnet_Membership]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [PathId] in table 'aspnet_Paths'
ALTER TABLE [dbo].[aspnet_Paths]
ADD CONSTRAINT [PK_aspnet_Paths]
    PRIMARY KEY CLUSTERED ([PathId] ASC);
GO

-- Creating primary key on [PathId] in table 'aspnet_PersonalizationAllUsers'
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]
ADD CONSTRAINT [PK_aspnet_PersonalizationAllUsers]
    PRIMARY KEY CLUSTERED ([PathId] ASC);
GO

-- Creating primary key on [Id] in table 'aspnet_PersonalizationPerUser'
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]
ADD CONSTRAINT [PK_aspnet_PersonalizationPerUser]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Profile'
ALTER TABLE [dbo].[aspnet_Profile]
ADD CONSTRAINT [PK_aspnet_Profile]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [RoleId] in table 'aspnet_Roles'
ALTER TABLE [dbo].[aspnet_Roles]
ADD CONSTRAINT [PK_aspnet_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [Feature], [CompatibleSchemaVersion] in table 'aspnet_SchemaVersions'
ALTER TABLE [dbo].[aspnet_SchemaVersions]
ADD CONSTRAINT [PK_aspnet_SchemaVersions]
    PRIMARY KEY CLUSTERED ([Feature], [CompatibleSchemaVersion] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Users'
ALTER TABLE [dbo].[aspnet_Users]
ADD CONSTRAINT [PK_aspnet_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [EventId] in table 'aspnet_WebEvent_Events'
ALTER TABLE [dbo].[aspnet_WebEvent_Events]
ADD CONSTRAINT [PK_aspnet_WebEvent_Events]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [ID] in table 'BankRecords'
ALTER TABLE [dbo].[BankRecords]
ADD CONSTRAINT [PK_BankRecords]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CashRecords'
ALTER TABLE [dbo].[CashRecords]
ADD CONSTRAINT [PK_CashRecords]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CategoryTypes'
ALTER TABLE [dbo].[CategoryTypes]
ADD CONSTRAINT [PK_CategoryTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'FixedAssets'
ALTER TABLE [dbo].[FixedAssets]
ADD CONSTRAINT [PK_FixedAssets]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'LedgerRecords'
ALTER TABLE [dbo].[LedgerRecords]
ADD CONSTRAINT [PK_LedgerRecords]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'LogTypes'
ALTER TABLE [dbo].[LogTypes]
ADD CONSTRAINT [PK_LogTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'OpeningBalances'
ALTER TABLE [dbo].[OpeningBalances]
ADD CONSTRAINT [PK_OpeningBalances]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Parameters'
ALTER TABLE [dbo].[Parameters]
ADD CONSTRAINT [PK_Parameters]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ProjectCategories'
ALTER TABLE [dbo].[ProjectCategories]
ADD CONSTRAINT [PK_ProjectCategories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [PK_Records]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'Types'
ALTER TABLE [dbo].[Types]
ADD CONSTRAINT [PK_Types]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [CategoryID], [CategoryName] in table 'CapitalCategories'
ALTER TABLE [dbo].[CapitalCategories]
ADD CONSTRAINT [PK_CapitalCategories]
    PRIMARY KEY CLUSTERED ([CategoryID], [CategoryName] ASC);
GO

-- Creating primary key on [CategoryID], [CategoryName] in table 'FixedAssetCategories'
ALTER TABLE [dbo].[FixedAssetCategories]
ADD CONSTRAINT [PK_FixedAssetCategories]
    PRIMARY KEY CLUSTERED ([CategoryID], [CategoryName] ASC);
GO

-- Creating primary key on [CategoryID], [CategoryName] in table 'RevenueCategories'
ALTER TABLE [dbo].[RevenueCategories]
ADD CONSTRAINT [PK_RevenueCategories]
    PRIMARY KEY CLUSTERED ([CategoryID], [CategoryName] ASC);
GO

-- Creating primary key on [Date], [VoucherNo] in table 'RevenueRecords'
ALTER TABLE [dbo].[RevenueRecords]
ADD CONSTRAINT [PK_RevenueRecords]
    PRIMARY KEY CLUSTERED ([Date], [VoucherNo] ASC);
GO

-- Creating primary key on [ApplicationName], [LoweredApplicationName], [ApplicationId] in table 'vw_aspnet_Applications'
ALTER TABLE [dbo].[vw_aspnet_Applications]
ADD CONSTRAINT [PK_vw_aspnet_Applications]
    PRIMARY KEY CLUSTERED ([ApplicationName], [LoweredApplicationName], [ApplicationId] ASC);
GO

-- Creating primary key on [UserId], [PasswordFormat], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [ApplicationId], [UserName], [IsAnonymous], [LastActivityDate] in table 'vw_aspnet_MembershipUsers'
ALTER TABLE [dbo].[vw_aspnet_MembershipUsers]
ADD CONSTRAINT [PK_vw_aspnet_MembershipUsers]
    PRIMARY KEY CLUSTERED ([UserId], [PasswordFormat], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [ApplicationId], [UserName], [IsAnonymous], [LastActivityDate] ASC);
GO

-- Creating primary key on [UserId], [LastUpdatedDate] in table 'vw_aspnet_Profiles'
ALTER TABLE [dbo].[vw_aspnet_Profiles]
ADD CONSTRAINT [PK_vw_aspnet_Profiles]
    PRIMARY KEY CLUSTERED ([UserId], [LastUpdatedDate] ASC);
GO

-- Creating primary key on [ApplicationId], [RoleId], [RoleName], [LoweredRoleName] in table 'vw_aspnet_Roles'
ALTER TABLE [dbo].[vw_aspnet_Roles]
ADD CONSTRAINT [PK_vw_aspnet_Roles]
    PRIMARY KEY CLUSTERED ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName] ASC);
GO

-- Creating primary key on [ApplicationId], [UserId], [UserName], [LoweredUserName], [IsAnonymous], [LastActivityDate] in table 'vw_aspnet_Users'
ALTER TABLE [dbo].[vw_aspnet_Users]
ADD CONSTRAINT [PK_vw_aspnet_Users]
    PRIMARY KEY CLUSTERED ([ApplicationId], [UserId], [UserName], [LoweredUserName], [IsAnonymous], [LastActivityDate] ASC);
GO

-- Creating primary key on [UserId], [RoleId] in table 'vw_aspnet_UsersInRoles'
ALTER TABLE [dbo].[vw_aspnet_UsersInRoles]
ADD CONSTRAINT [PK_vw_aspnet_UsersInRoles]
    PRIMARY KEY CLUSTERED ([UserId], [RoleId] ASC);
GO

-- Creating primary key on [ApplicationId], [PathId], [Path], [LoweredPath] in table 'vw_aspnet_WebPartState_Paths'
ALTER TABLE [dbo].[vw_aspnet_WebPartState_Paths]
ADD CONSTRAINT [PK_vw_aspnet_WebPartState_Paths]
    PRIMARY KEY CLUSTERED ([ApplicationId], [PathId], [Path], [LoweredPath] ASC);
GO

-- Creating primary key on [PathId], [LastUpdatedDate] in table 'vw_aspnet_WebPartState_Shared'
ALTER TABLE [dbo].[vw_aspnet_WebPartState_Shared]
ADD CONSTRAINT [PK_vw_aspnet_WebPartState_Shared]
    PRIMARY KEY CLUSTERED ([PathId], [LastUpdatedDate] ASC);
GO

-- Creating primary key on [LastUpdatedDate] in table 'vw_aspnet_WebPartState_User'
ALTER TABLE [dbo].[vw_aspnet_WebPartState_User]
ADD CONSTRAINT [PK_vw_aspnet_WebPartState_User]
    PRIMARY KEY CLUSTERED ([LastUpdatedDate] ASC);
GO

-- Creating primary key on [aspnet_Roles_RoleId], [aspnet_Users_UserId] in table 'aspnet_UsersInRoles'
ALTER TABLE [dbo].[aspnet_UsersInRoles]
ADD CONSTRAINT [PK_aspnet_UsersInRoles]
    PRIMARY KEY NONCLUSTERED ([aspnet_Roles_RoleId], [aspnet_Users_UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ApplicationId] in table 'aspnet_Membership'
ALTER TABLE [dbo].[aspnet_Membership]
ADD CONSTRAINT [FK__aspnet_Me__Appli__0C85DE4D]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Me__Appli__0C85DE4D'
CREATE INDEX [IX_FK__aspnet_Me__Appli__0C85DE4D]
ON [dbo].[aspnet_Membership]
    ([ApplicationId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Paths'
ALTER TABLE [dbo].[aspnet_Paths]
ADD CONSTRAINT [FK__aspnet_Pa__Appli__45BE5BA9]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Pa__Appli__45BE5BA9'
CREATE INDEX [IX_FK__aspnet_Pa__Appli__45BE5BA9]
ON [dbo].[aspnet_Paths]
    ([ApplicationId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Roles'
ALTER TABLE [dbo].[aspnet_Roles]
ADD CONSTRAINT [FK__aspnet_Ro__Appli__2EDAF651]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Ro__Appli__2EDAF651'
CREATE INDEX [IX_FK__aspnet_Ro__Appli__2EDAF651]
ON [dbo].[aspnet_Roles]
    ([ApplicationId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Users'
ALTER TABLE [dbo].[aspnet_Users]
ADD CONSTRAINT [FK__aspnet_Us__Appli__787EE5A0]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Us__Appli__787EE5A0'
CREATE INDEX [IX_FK__aspnet_Us__Appli__787EE5A0]
ON [dbo].[aspnet_Users]
    ([ApplicationId]);
GO

-- Creating foreign key on [UserId] in table 'aspnet_Membership'
ALTER TABLE [dbo].[aspnet_Membership]
ADD CONSTRAINT [FK__aspnet_Me__UserI__0D7A0286]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PathId] in table 'aspnet_PersonalizationAllUsers'
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]
ADD CONSTRAINT [FK__aspnet_Pe__PathI__4D5F7D71]
    FOREIGN KEY ([PathId])
    REFERENCES [dbo].[aspnet_Paths]
        ([PathId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PathId] in table 'aspnet_PersonalizationPerUser'
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]
ADD CONSTRAINT [FK__aspnet_Pe__PathI__531856C7]
    FOREIGN KEY ([PathId])
    REFERENCES [dbo].[aspnet_Paths]
        ([PathId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Pe__PathI__531856C7'
CREATE INDEX [IX_FK__aspnet_Pe__PathI__531856C7]
ON [dbo].[aspnet_PersonalizationPerUser]
    ([PathId]);
GO

-- Creating foreign key on [UserId] in table 'aspnet_PersonalizationPerUser'
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]
ADD CONSTRAINT [FK__aspnet_Pe__UserI__540C7B00]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Pe__UserI__540C7B00'
CREATE INDEX [IX_FK__aspnet_Pe__UserI__540C7B00]
ON [dbo].[aspnet_PersonalizationPerUser]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'aspnet_Profile'
ALTER TABLE [dbo].[aspnet_Profile]
ADD CONSTRAINT [FK__aspnet_Pr__UserI__236943A5]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RecordID] in table 'BankRecords'
ALTER TABLE [dbo].[BankRecords]
ADD CONSTRAINT [FK_BankRecord_Record]
    FOREIGN KEY ([RecordID])
    REFERENCES [dbo].[Records]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BankRecord_Record'
CREATE INDEX [IX_FK_BankRecord_Record]
ON [dbo].[BankRecords]
    ([RecordID]);
GO

-- Creating foreign key on [CategoryID] in table 'ProjectCategories'
ALTER TABLE [dbo].[ProjectCategories]
ADD CONSTRAINT [FK_ProjectCategory_Category]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[Categories]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectCategory_Category'
CREATE INDEX [IX_FK_ProjectCategory_Category]
ON [dbo].[ProjectCategories]
    ([CategoryID]);
GO

-- Creating foreign key on [RecordID] in table 'FixedAssets'
ALTER TABLE [dbo].[FixedAssets]
ADD CONSTRAINT [FK_FixedAsset_Record]
    FOREIGN KEY ([RecordID])
    REFERENCES [dbo].[Records]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FixedAsset_Record'
CREATE INDEX [IX_FK_FixedAsset_Record]
ON [dbo].[FixedAssets]
    ([RecordID]);
GO

-- Creating foreign key on [ProjectCategoryID] in table 'OpeningBalances'
ALTER TABLE [dbo].[OpeningBalances]
ADD CONSTRAINT [FK_OpeningBalance_ProjectCategory]
    FOREIGN KEY ([ProjectCategoryID])
    REFERENCES [dbo].[ProjectCategories]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OpeningBalance_ProjectCategory'
CREATE INDEX [IX_FK_OpeningBalance_ProjectCategory]
ON [dbo].[OpeningBalances]
    ([ProjectCategoryID]);
GO

-- Creating foreign key on [ProjectID] in table 'ProjectCategories'
ALTER TABLE [dbo].[ProjectCategories]
ADD CONSTRAINT [FK_ProjectCategory_Project]
    FOREIGN KEY ([ProjectID])
    REFERENCES [dbo].[Projects]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectCategory_Project'
CREATE INDEX [IX_FK_ProjectCategory_Project]
ON [dbo].[ProjectCategories]
    ([ProjectID]);
GO

-- Creating foreign key on [ProjectCategoryID] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_Record_ProjectCategory]
    FOREIGN KEY ([ProjectCategoryID])
    REFERENCES [dbo].[ProjectCategories]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Record_ProjectCategory'
CREATE INDEX [IX_FK_Record_ProjectCategory]
ON [dbo].[Records]
    ([ProjectCategoryID]);
GO

-- Creating foreign key on [aspnet_Roles_RoleId] in table 'aspnet_UsersInRoles'
ALTER TABLE [dbo].[aspnet_UsersInRoles]
ADD CONSTRAINT [FK_aspnet_UsersInRoles_aspnet_Roles]
    FOREIGN KEY ([aspnet_Roles_RoleId])
    REFERENCES [dbo].[aspnet_Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [aspnet_Users_UserId] in table 'aspnet_UsersInRoles'
ALTER TABLE [dbo].[aspnet_UsersInRoles]
ADD CONSTRAINT [FK_aspnet_UsersInRoles_aspnet_Users]
    FOREIGN KEY ([aspnet_Users_UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_aspnet_UsersInRoles_aspnet_Users'
CREATE INDEX [IX_FK_aspnet_UsersInRoles_aspnet_Users]
ON [dbo].[aspnet_UsersInRoles]
    ([aspnet_Users_UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------