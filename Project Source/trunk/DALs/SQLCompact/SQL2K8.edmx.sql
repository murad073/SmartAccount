
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/23/2012 20:15:18
-- Generated from EDMX file: D:\My Projects\Practice self\.net projects\office Github SmartAccount\Project Source\trunk\DALs\SQLCE\SQLCE.edmx
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

IF OBJECT_ID(N'[dbo].[FK_BankRecord_Record]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BankRecord] DROP CONSTRAINT [FK_BankRecord_Record];
GO
IF OBJECT_ID(N'[dbo].[FK_Budget_ProjectHead]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Budget] DROP CONSTRAINT [FK_Budget_ProjectHead];
GO
IF OBJECT_ID(N'[dbo].[FK_FixedAsset_Record]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FixedAsset] DROP CONSTRAINT [FK_FixedAsset_Record];
GO
IF OBJECT_ID(N'[dbo].[FK_OpeningBalance_ProjectCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OpeningBalance] DROP CONSTRAINT [FK_OpeningBalance_ProjectCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectHead_Head]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectHead] DROP CONSTRAINT [FK_ProjectHead_Head];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectHead_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectHead] DROP CONSTRAINT [FK_ProjectHead_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_Record_ProjectCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_Record_ProjectCategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BankRecord]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BankRecord];
GO
IF OBJECT_ID(N'[dbo].[Budget]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Budget];
GO
IF OBJECT_ID(N'[dbo].[FixedAsset]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FixedAsset];
GO
IF OBJECT_ID(N'[dbo].[Head]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Head];
GO
IF OBJECT_ID(N'[dbo].[Log]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Log];
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
IF OBJECT_ID(N'[dbo].[ProjectHead]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectHead];
GO
IF OBJECT_ID(N'[dbo].[Record]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Record];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BankRecords'
CREATE TABLE [dbo].[BankRecords] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecordID] int  NOT NULL,
    [ChequeNo] varchar(500)  NULL,
    [BankName] varchar(500)  NULL,
    [Branch] varchar(500)  NULL,
    [ChequeDate] datetime  NULL
);
GO

-- Creating table 'FixedAssets'
CREATE TABLE [dbo].[FixedAssets] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(500)  NULL,
    [DepreciationRate] float  NULL,
    [DepreciatedValue] int  NULL,
    [DepreciationType] varchar(50)  NULL,
    [ByForceDisposed] bit  NULL,
    [RecordID] int  NULL
);
GO

-- Creating table 'Heads'
CREATE TABLE [dbo].[Heads] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ParentID] int  NULL,
    [Name] varchar(500)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Type] varchar(50)  NOT NULL,
    [Description] varchar(max)  NULL
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

-- Creating table 'OpeningBalances'
CREATE TABLE [dbo].[OpeningBalances] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [ProjectHeadID] int  NULL,
    [Balance] int  NULL,
    [IsActive] bit  NOT NULL,
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
    [IsActive] bit  NOT NULL,
    [Name] varchar(500)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [Description] varchar(max)  NULL
);
GO

-- Creating table 'ProjectHeads'
CREATE TABLE [dbo].[ProjectHeads] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProjectID] int  NOT NULL,
    [HeadID] int  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Records'
CREATE TABLE [dbo].[Records] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProjectHeadID] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [VoucherType] varchar(500)  NOT NULL,
    [Debit] float  NOT NULL,
    [Credit] float  NOT NULL,
    [Narration] varchar(max)  NULL,
    [LedgerType] varchar(50)  NOT NULL,
    [VoucherSerialNo] int  NOT NULL,
    [Link] varchar(100)  NULL,
    [Tag] varchar(max)  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Budgets'
CREATE TABLE [dbo].[Budgets] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProjectHeadID] int  NULL,
    [Date] datetime  NULL,
    [Amount] float  NOT NULL,
    [Note] varchar(max)  NULL,
    [IsActive] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'BankRecords'
ALTER TABLE [dbo].[BankRecords]
ADD CONSTRAINT [PK_BankRecords]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'FixedAssets'
ALTER TABLE [dbo].[FixedAssets]
ADD CONSTRAINT [PK_FixedAssets]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Heads'
ALTER TABLE [dbo].[Heads]
ADD CONSTRAINT [PK_Heads]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
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

-- Creating primary key on [ID] in table 'ProjectHeads'
ALTER TABLE [dbo].[ProjectHeads]
ADD CONSTRAINT [PK_ProjectHeads]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [PK_Records]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Budgets'
ALTER TABLE [dbo].[Budgets]
ADD CONSTRAINT [PK_Budgets]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [HeadID] in table 'ProjectHeads'
ALTER TABLE [dbo].[ProjectHeads]
ADD CONSTRAINT [FK_ProjectHead_Head]
    FOREIGN KEY ([HeadID])
    REFERENCES [dbo].[Heads]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectHead_Head'
CREATE INDEX [IX_FK_ProjectHead_Head]
ON [dbo].[ProjectHeads]
    ([HeadID]);
GO

-- Creating foreign key on [ProjectHeadID] in table 'OpeningBalances'
ALTER TABLE [dbo].[OpeningBalances]
ADD CONSTRAINT [FK_OpeningBalance_ProjectCategory]
    FOREIGN KEY ([ProjectHeadID])
    REFERENCES [dbo].[ProjectHeads]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OpeningBalance_ProjectCategory'
CREATE INDEX [IX_FK_OpeningBalance_ProjectCategory]
ON [dbo].[OpeningBalances]
    ([ProjectHeadID]);
GO

-- Creating foreign key on [ProjectID] in table 'ProjectHeads'
ALTER TABLE [dbo].[ProjectHeads]
ADD CONSTRAINT [FK_ProjectHead_Project]
    FOREIGN KEY ([ProjectID])
    REFERENCES [dbo].[Projects]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectHead_Project'
CREATE INDEX [IX_FK_ProjectHead_Project]
ON [dbo].[ProjectHeads]
    ([ProjectID]);
GO

-- Creating foreign key on [ProjectHeadID] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_Record_ProjectCategory]
    FOREIGN KEY ([ProjectHeadID])
    REFERENCES [dbo].[ProjectHeads]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Record_ProjectCategory'
CREATE INDEX [IX_FK_Record_ProjectCategory]
ON [dbo].[Records]
    ([ProjectHeadID]);
GO

-- Creating foreign key on [ProjectHeadID] in table 'Budgets'
ALTER TABLE [dbo].[Budgets]
ADD CONSTRAINT [FK_Budget_ProjectHead]
    FOREIGN KEY ([ProjectHeadID])
    REFERENCES [dbo].[ProjectHeads]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Budget_ProjectHead'
CREATE INDEX [IX_FK_Budget_ProjectHead]
ON [dbo].[Budgets]
    ([ProjectHeadID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------