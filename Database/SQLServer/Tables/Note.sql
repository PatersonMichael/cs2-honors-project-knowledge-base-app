USE KnowledgeBase;
GO

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