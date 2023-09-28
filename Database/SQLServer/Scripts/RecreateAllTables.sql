USE KnowledgeBase;
GO

-- !!!!!!!!!!!!!!!!!!!! WARNING: This will delete all tables in the database, only do this if working on a dev/test version of the DB !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

-- Drop existing tables 

DROP TABLE IF EXISTS KnowledgeBase.SourceMaterial, KnowledgeBase.UserProfile;

-- Create Tables

-- UserProfile

CREATE TABLE KnowledgeBase.UserProfile (
    UserProfileID INT IDENTITY (1,1) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(320) NOT NULL,
    CreationDate DATETIME2(0) NOT NULL,
    BirthDate DATE NOT NULL,
    CONSTRAINT PK_UserProfile_UserProfileID PRIMARY KEY CLUSTERED (UserProfileID ASC),
    CONSTRAINT AK_UserProfile_Email UNIQUE NONCLUSTERED (Email)
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO

-- Source Material

CREATE TABLE KnowledgeBase.SourceMaterial (
    SourceMaterialID INT IDENTITY (1,1) NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    PublishDate DATE NOT NULL,
    Publisher NVARCHAR(200) NULL,
    SourceMaterialType VARCHAR(50) NOT NULL,
    SourceMaterialEdition VARCHAR(30) NULL,
    UserProfileID INT NOT NULL,
    CONSTRAINT PK_SourceMaterial_SourceMaterialID PRIMARY KEY CLUSTERED (SourceMaterialID ASC),
    CONSTRAINT AK_SourceMaterial_Title_SourceMaterialEdition UNIQUE NONCLUSTERED (Title, SourceMaterialEdition),
    CONSTRAINT FK_SourceMaterial_UserProfileID_UserProfile_UserProfileID
        FOREIGN KEY (UserProfileID) REFERENCES KnowledgeBase.UserProfile(UserProfileID)
        ON DELETE CASCADE
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO