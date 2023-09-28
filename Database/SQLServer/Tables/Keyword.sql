USE KnowledgeBase;
GO

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