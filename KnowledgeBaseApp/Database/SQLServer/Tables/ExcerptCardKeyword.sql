USE KnowledgeBase;
GO

CREATE TABLE KnowledgeBase.ExcerptCardKeyword (
    ExcerptCardID int NOT NULL,
    KeywordID int NOT NULL
    CONSTRAINT FK_ExcerptCardKeyword_ExcerptCardID_ExcerptCard_ExcerptCardID
        FOREIGN KEY (ExcerptCardID) REFERENCES KnowledgeBase.ExcerptCard (ExcerptCardID)
            ON DELETE CASCADE,
    CONSTRAINT FK_ExcerptCardKeyword_KeywordID_Keyword_KeywordID
        FOREIGN KEY (KeywordID) REFERENCES KnowledgeBase.Keyword (KeywordID)
            ON DELETE CASCADE
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO