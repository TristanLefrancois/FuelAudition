
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/18/2016 22:04:28
-- Generated from EDMX file: C:\Users\AdminLocal\Desktop\SiteDevMathieu\FuelAudition\Models\FuelAuditionModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FuelAudition];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Client_Langue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Client] DROP CONSTRAINT [FK_Client_Langue];
GO
IF OBJECT_ID(N'[dbo].[FK_Client_Ville]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Client] DROP CONSTRAINT [FK_Client_Ville];
GO
IF OBJECT_ID(N'[dbo].[FK_Province_Pays]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Province] DROP CONSTRAINT [FK_Province_Pays];
GO
IF OBJECT_ID(N'[dbo].[FK_Ville_Province]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ville] DROP CONSTRAINT [FK_Ville_Province];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Client];
GO
IF OBJECT_ID(N'[dbo].[Langue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Langue];
GO
IF OBJECT_ID(N'[dbo].[Pays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pays];
GO
IF OBJECT_ID(N'[dbo].[Province]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Province];
GO
IF OBJECT_ID(N'[dbo].[Ville]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ville];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Client'
CREATE TABLE [dbo].[Client] (
    [ClientId] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(100)  NOT NULL,
    [Telephone] nvarchar(50)  NULL,
    [Inter] nvarchar(5)  NULL,
    [FAX] nvarchar(50)  NULL,
    [Email] nvarchar(250)  NULL,
    [Adresse1] nvarchar(100)  NULL,
    [Adresse2] nvarchar(50)  NULL,
    [VilleId] int  NULL,
    [CodePostal] nvarchar(10)  NULL,
    [ContactNom] nvarchar(50)  NULL,
    [ContactTelephone] nvarchar(50)  NULL,
    [ContactInter] nvarchar(5)  NULL,
    [ContactEmail] nvarchar(250)  NULL,
    [LangueId] int  NULL,
    [Actif] bit  NOT NULL
);
GO

-- Creating table 'Langue'
CREATE TABLE [dbo].[Langue] (
    [LangueId] int IDENTITY(1,1) NOT NULL,
    [LangueCode] nvarchar(2)  NOT NULL,
    [NomFr] nvarchar(50)  NOT NULL,
    [NomAn] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Pays'
CREATE TABLE [dbo].[Pays] (
    [PaysId] int IDENTITY(1,1) NOT NULL,
    [PaysCode] nvarchar(5)  NOT NULL,
    [NomFr] nvarchar(50)  NOT NULL,
    [NomAn] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Province'
CREATE TABLE [dbo].[Province] (
    [ProvinceId] int IDENTITY(1,1) NOT NULL,
    [ProvinceCode] nvarchar(2)  NOT NULL,
    [NomFr] nvarchar(50)  NOT NULL,
    [NomAn] nvarchar(50)  NOT NULL,
    [PaysId] int  NULL
);
GO

-- Creating table 'Ville'
CREATE TABLE [dbo].[Ville] (
    [VilleId] int IDENTITY(1,1) NOT NULL,
    [VilleCode] int  NOT NULL,
    [Nom] nvarchar(255)  NOT NULL,
    [Désignation] nvarchar(255)  NOT NULL,
    [MRCCM] nvarchar(255)  NOT NULL,
    [Région] nvarchar(255)  NOT NULL,
    [ProvinceId] int  NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NOT NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [ClientId] int  NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClientId] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [PK_Client]
    PRIMARY KEY CLUSTERED ([ClientId] ASC);
GO

-- Creating primary key on [LangueId] in table 'Langue'
ALTER TABLE [dbo].[Langue]
ADD CONSTRAINT [PK_Langue]
    PRIMARY KEY CLUSTERED ([LangueId] ASC);
GO

-- Creating primary key on [PaysId] in table 'Pays'
ALTER TABLE [dbo].[Pays]
ADD CONSTRAINT [PK_Pays]
    PRIMARY KEY CLUSTERED ([PaysId] ASC);
GO

-- Creating primary key on [ProvinceId] in table 'Province'
ALTER TABLE [dbo].[Province]
ADD CONSTRAINT [PK_Province]
    PRIMARY KEY CLUSTERED ([ProvinceId] ASC);
GO

-- Creating primary key on [VilleId] in table 'Ville'
ALTER TABLE [dbo].[Ville]
ADD CONSTRAINT [PK_Ville]
    PRIMARY KEY CLUSTERED ([VilleId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LangueId] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [FK_Client_Langue]
    FOREIGN KEY ([LangueId])
    REFERENCES [dbo].[Langue]
        ([LangueId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Client_Langue'
CREATE INDEX [IX_FK_Client_Langue]
ON [dbo].[Client]
    ([LangueId]);
GO

-- Creating foreign key on [VilleId] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [FK_Client_Ville]
    FOREIGN KEY ([VilleId])
    REFERENCES [dbo].[Ville]
        ([VilleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Client_Ville'
CREATE INDEX [IX_FK_Client_Ville]
ON [dbo].[Client]
    ([VilleId]);
GO

-- Creating foreign key on [PaysId] in table 'Province'
ALTER TABLE [dbo].[Province]
ADD CONSTRAINT [FK_Province_Pays]
    FOREIGN KEY ([PaysId])
    REFERENCES [dbo].[Pays]
        ([PaysId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Province_Pays'
CREATE INDEX [IX_FK_Province_Pays]
ON [dbo].[Province]
    ([PaysId]);
GO

-- Creating foreign key on [ProvinceId] in table 'Ville'
ALTER TABLE [dbo].[Ville]
ADD CONSTRAINT [FK_Ville_Province]
    FOREIGN KEY ([ProvinceId])
    REFERENCES [dbo].[Province]
        ([ProvinceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Ville_Province'
CREATE INDEX [IX_FK_Ville_Province]
ON [dbo].[Ville]
    ([ProvinceId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------