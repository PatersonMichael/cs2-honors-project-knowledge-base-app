USE KnowledgeBase;
GO

CREATE TABLE KnowledgeBase.SourceMaterialAuthor (
    SourceMaterialID INT NOT NULL,
    AuthorID INT NOT NULL,
    CONSTRAINT PK_SourceMaterialAuthor_SourceMaterialID_AuthorID PRIMARY KEY CLUSTERED (SourceMaterialID ASC, AuthorID),
    CONSTRAINT FK_SourceMaterialAuthor_SourceMaterialID_SourceMaterial_SourceMaterialID
        FOREIGN KEY (SourceMaterialID) REFERENCES KnowledgeBase.SourceMaterial (SourceMaterialID)
            ON DELETE CASCADE,
    CONSTRAINT FK_SourceMaterialAuthor_AuthorID_Author_AuthorID
        FOREIGN KEY (AuthorID) REFERENCES KnowledgeBase.Author (AuthorID)
            ON DELETE CASCADE
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO