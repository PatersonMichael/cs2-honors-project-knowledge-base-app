USE KnowledgeBase;
GO

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