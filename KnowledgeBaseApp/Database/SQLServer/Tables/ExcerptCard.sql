USE KnowledgeBase;
GO

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
            ON DELETE SET NULL
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO