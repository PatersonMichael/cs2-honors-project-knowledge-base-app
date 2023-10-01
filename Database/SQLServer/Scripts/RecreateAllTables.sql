USE KnowledgeBase;
GO

-- !!!!!!!!!!!!!!!!!!!! WARNING: This will delete all tables in the database, only do this if working on a dev/test version of the DB !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

-- Drop existing tables 

DROP TABLE IF EXISTS KnowledgeBase.NoteKeyword, KnowledgeBase.ExcerptCardKeyword, KnowledgeBase.Keyword, KnowledgeBase.Note, KnowledgeBase.ExcerptCard,
    KnowledgeBase.Citation, KnowledgeBase.SourceMaterialAuthor, KnowledgeBase.Author, KnowledgeBase.SourceMaterial, KnowledgeBase.UserProfile;

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

-- Author

CREATE TABLE KnowledgeBase.Author (
    AuthorID INT IDENTITY (1,1) NOT NULL,
    FirstName NVARCHAR(200) NOT NULL,
    LastName NVARCHAR(200) NOT NULL,
    UserProfileID INT NOT NULL
    CONSTRAINT PK_Author_AuthorID PRIMARY KEY CLUSTERED (AuthorID),
    CONSTRAINT FK_Author_UserProfileID_UserProfile_UserProfileID
        FOREIGN KEY (UserProfileID) REFERENCES KnowledgeBase.UserProfile (UserProfileID)
            ON DELETE CASCADE
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO

-- Source Material Author

CREATE TABLE KnowledgeBase.SourceMaterialAuthor (
    SourceMaterialID INT NOT NULL,
    AuthorID INT NOT NULL,
    CONSTRAINT PK_SourceMaterialAuthor_SourceMaterialID_AuthorID PRIMARY KEY CLUSTERED (SourceMaterialID ASC, AuthorID),
    CONSTRAINT FK_SourceMaterialAuthor_SourceMaterialID_SourceMaterial_SourceMaterialID
        FOREIGN KEY (SourceMaterialID) REFERENCES KnowledgeBase.SourceMaterial (SourceMaterialID)
            ON DELETE CASCADE,
    CONSTRAINT FK_SourceMaterialAuthor_AuthorID_Author_AuthorID
        FOREIGN KEY (AuthorID) REFERENCES KnowledgeBase.Author (AuthorID)
            ON DELETE NO ACTION
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO

-- Citation

CREATE TABLE KnowledgeBase.Citation (
    CitationID INT IDENTITY (1,1) NOT NULL,
    Format VARCHAR(50) NOT NULL,
    ExcerptLocation VARCHAR(40),
    CreationDate DATE NOT NULL,
    UserProfileID INT NOT NULL,
    SourceMaterialID INT NULL,
    CONSTRAINT PK_Citation_CitationID PRIMARY KEY CLUSTERED (CitationID ASC),
    CONSTRAINT FK_Citation_UserProfileID_UserProfile_UserProfileID
        FOREIGN KEY (UserProfileID) REFERENCES KnowledgeBase.UserProfile (UserProfileID)
            ON DELETE CASCADE,
    CONSTRAINT FK_Citation_SourceMaterialID_SourceMaterial_SourceMaterialID 
        FOREIGN KEY (SourceMaterialID) REFERENCES KnowledgeBase.SourceMaterial (SourceMaterialID)
            ON DELETE NO ACTION
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO

-- Excerpt Card

CREATE TABLE KnowledgeBase.ExcerptCard (
    ExcerptCardID INT IDENTITY (1,1) NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Excerpt NVARCHAR(800) NULL,
    CreationDate DATETIME2(2) NOT NULL,
    LastUpdatedDate DATETIME2(2) NOT NULL,
    UserProfileID INT NOT NULL,
    CitationID INT NULL,
    CONSTRAINT PK_ExcerptCard_ExcerptCardID PRIMARY KEY CLUSTERED (ExcerptCardID ASC),
    CONSTRAINT FK_ExcerptCard_UserProfileID_UserProfile_UserProfileID
        FOREIGN KEY (UserProfileID) REFERENCES KnowledgeBase.UserProfile (UserProfileID)
            ON DELETE CASCADE,
    CONSTRAINT FK_ExcerptCard_CitationID_Citation_CitationID
        FOREIGN KEY (CitationID) REFERENCES KnowledgeBase.Citation (CitationID)
            ON DELETE NO ACTION
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO

-- Keyword

CREATE TABLE KnowledgeBase.Keyword (
    KeywordID INT IDENTITY (1,1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
    UserProfileID INT NOT NULL,
    CONSTRAINT PK_Keyword_KeywordID PRIMARY KEY CLUSTERED (KeywordID ASC),
    CONSTRAINT UQ_Keyword_Name UNIQUE NONCLUSTERED ([Name]),
    CONSTRAINT FK_Keyword_UserProfileID_UserProfile_UserProfileID
        FOREIGN KEY (UserProfileID) REFERENCES KnowledgeBase.UserProfile (UserProfileID)
            ON DELETE CASCADE
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO

-- Excerpt Card Keyword

CREATE TABLE KnowledgeBase.ExcerptCardKeyword (
    ExcerptCardID int NOT NULL,
    KeywordID int NOT NULL
    CONSTRAINT FK_ExcerptCardKeyword_ExcerptCardID_ExcerptCard_ExcerptCardID
        FOREIGN KEY (ExcerptCardID) REFERENCES KnowledgeBase.ExcerptCard (ExcerptCardID)
            ON DELETE CASCADE,
    CONSTRAINT FK_ExcerptCardKeyword_KeywordID_Keyword_KeywordID
        FOREIGN KEY (KeywordID) REFERENCES KnowledgeBase.Keyword (KeywordID)
            ON DELETE NO ACTION
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO

-- Note

CREATE TABLE KnowledgeBase.Note (
    NoteID INT IDENTITY (1,1) NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Body NVARCHAR(MAX) NULL,
    CreationDate DATE NOT NULL,
    LastUpdatedDate DATE NOT NULL,
    UserProfileID INT NOT NULL,
    CONSTRAINT PK_Note_NoteID PRIMARY KEY CLUSTERED (NoteID ASC),
    CONSTRAINT FK_Note_UserProfileID_UserProfile_UserProfileID
        FOREIGN KEY (UserProfileID) REFERENCES KnowledgeBase.UserProfile (UserProfileID)
        ON DELETE CASCADE
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO

-- Note Keyword

CREATE TABLE KnowledgeBase.NoteKeyword (
    NoteID int NOT NULL,
    KeywordID int NOT NULL
    CONSTRAINT PK_NoteKeyword_NoteID_KeywordID PRIMARY KEY CLUSTERED (NoteID ASC, KeywordID)
    CONSTRAINT FK_NoteKeyword_ExcerptCardID_Note_NoteID
        FOREIGN KEY (NoteID) REFERENCES KnowledgeBase.Note (NoteID)
            ON DELETE CASCADE,
    CONSTRAINT FK_NoteKeyword_KeywordID_Keyword_KeywordID
        FOREIGN KEY (KeywordID) REFERENCES KnowledgeBase.Keyword (KeywordID)
            ON DELETE NO ACTION
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO



