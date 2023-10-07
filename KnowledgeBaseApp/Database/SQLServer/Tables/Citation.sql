USE KnowledgeBase;
GO

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
            ON DELETE SET NULL
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO