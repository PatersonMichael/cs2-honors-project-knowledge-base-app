USE KnowledgeBase;
GO

--DROP TABLE IF EXISTS KnowledgeBase.SourceMaterial, KnowledgeBase.UserProfile;

CREATE TABLE KnowledgeBase.UserProfile (
    UserProfileID INT IDENTITY (1,1) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(320) NOT NULL,
    CreationDate DATETIME2(0) NOT NULL,
    BirthDate DATE NOT NULL,
    Nametag NVARCHAR(60) NOT NULL,
    UserProfilePassword VARCHAR(256) NOT NULL,
    CONSTRAINT UQ_UserProfile_Nametag UNIQUE NONCLUSTERED (Nametag),
    CONSTRAINT PK_UserProfile_UserProfileID PRIMARY KEY CLUSTERED (UserProfileID ASC),
    CONSTRAINT AK_UserProfile_Email UNIQUE NONCLUSTERED (Email)
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO
