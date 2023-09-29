USE KnowledgeBase;
GO

CREATE TABLE KnowledgeBase.NoteKeyword (
    NoteID int NOT NULL,
    KeywordID int NOT NULL
    CONSTRAINT PK_NoteKeyword_NoteID_KeywordID PRIMARY KEY CLUSTERED (NoteID ASC, KeywordID)
    CONSTRAINT FK_NoteKeyword_ExcerptCardID_Note_NoteID
        FOREIGN KEY (NoteID) REFERENCES KnowledgeBase.Note (NoteID)
            ON DELETE CASCADE,
    CONSTRAINT FK_NoteKeyword_KeywordID_Keyword_KeywordID
        FOREIGN KEY (KeywordID) REFERENCES KnowledgeBase.Keyword (KeywordID)
            ON DELETE CASCADE
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO