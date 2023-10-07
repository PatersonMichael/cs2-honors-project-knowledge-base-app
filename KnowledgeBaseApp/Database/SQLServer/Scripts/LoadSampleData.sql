USE KnowledgeBase;
GO

-- Before running this script you may want to run RecreateAllTables WARNING!!!!! This will delete all tables in the database, only do this if working on a dev/test version of the DB !!!!!!!!

-- UserProfiles --

INSERT INTO KnowledgeBase.UserProfile (
    FirstName,
    LastName,
    Email,
    CreationDate,
    BirthDate,
    Nametag
)
VALUES
    ('John', 'Smith', 'JSmith@Sample.com', '20230908', '19980929', 'theCoolestJ$mith'),
    ('Sarah', 'Smith', 'SSmith@Sample.com', '20230908', '19890924', 'wifeySarah'),
    ('Gabe', 'Oliver', 'NotyourFriend27@gweegel.org', '20230908', '20000117', 'NotYourFriend27'),
    ('Nickel', 'Beck', 'GrossGuy@bahoo.com', '20230908', '20130522', 'Goober98');

GO

-- SourceMaterials --

INSERT INTO KnowledgeBase.SourceMaterial (
    Title,
    PublishDate,
    Publisher,
    SourceMaterialType,
    SourceMaterialEdition,
    UserProfileID
)
VALUES
    ('The Lord of the Rings: Fellowship of the Ring', '19540729', 'Houghton Mifflin Harcourt', 'Book', '1982', 3),
    ('The Lord of the Rings: Fellowship of the Ring', '19540729', 'Houghton Mifflin Harcourt', 'Book', '1954', 2),
    ('Ethics, the Fundamentals', '2007', 'Blackwell Publishing', 'Book', '2007', 2),
    ('Local Hotdog Stand Owner Leads Movement To End Squirrel Watching', '20220327', 'Washington Post', 'News Article', '20230925', 4);

GO

-- Citation

INSERT INTO KnowledgeBase.Citation (
    Format,
    ExcerptLocation,
    CreationDate,
    UserProfileID,
    SourceMaterialID
)

    VALUES
        ('MLA', 'pg. 44', '20230930', 1, 1),
        ('APA', 'paragraph 11', '20230415', 4, 4);

-- Excerpt Card

INSERT INTO KnowledgeBase.ExcerptCard (
    Title,
    Excerpt,
    CreationDate,
    LastUpdatedDate,
    UserProfileID,
    CitationID
)
VALUES
    ('Sam mentions the undying lands', 'They are sailing, sailing, sailing over the Sea, they are going into the west and leaving us.', '20230930', '20230930', 1, 1),
    ('The hotdog stand owner''s words on squirrels', 'They''re plotting to destroy us all!', '20230415', '20230501', 4, 2);

    -- Keywords

INSERT INTO KnowledgeBase.Keyword (
    [Name],
    UserProfileID
)
VALUES
    ('elves', 1),
    ('into the west', 1),
    ('sam', 1),
    ('squirrels', 4),
    ('evil', 4),
    ('evil squirrels', 4);

    -- Excerpt Card Keywords

INSERT INTO KnowledgeBase.ExcerptCardKeyword (
    ExcerptCardID,
    KeywordID
)
VALUES
    (1, 1),
    (1, 2),
    (1, 3),
    (1, 5),
    (2, 4),
    (2, 5),
    (2, 6);

-- Note

INSERT INTO KnowledgeBase.Note (
    Title,
    Body,
    CreationDate,
    LastUpdatedDate,
    UserProfileID
)
VALUES
    ('Why are squirrels so evil?', 'Based on the article I saw about the hotdog stand owner, 
        I need to figure out the source of squirrel evil. Is it sauron? 
        Are the elves leaving because of the squirrels?',
        '20230502', '20230502', 4);

INSERT INTO KnowledgeBase.NoteKeyword (
    NoteID,
    KeywordID
)
VALUES
    (1, 4),
    (1, 1),
    (1, 5);

INSERT INTO KnowledgeBase.Author (
    FirstName,
    LastName,
    UserProfileID
)
VALUES
    ('JRR', 'Tolkien', 2),
    ('Julia', 'Driver', 2),
    ('Shmiddleby', 'Biggle', 4);

INSERT INTO KnowledgeBase.SourceMaterialAuthor (
    SourceMaterialID,
    AuthorID
)

VALUES
    (1, 1),
    (2, 1),
    (3, 2),
    (4, 3);

------------------------------------------------------------------ Sample Queries --------------------------------------------------------------------

-- View entire Tables

SELECT * FROM KnowledgeBase.UserProfile;
SELECT * FROM KnowledgeBase.SourceMaterial;
SELECT * FROM KnowledgeBase.Citation;
SELECT * FROM KnowledgeBase.ExcerptCard;
SELECT * FROM KnowledgeBase.Note;

-- Test query to see what users added each source material, and each source material's author

SELECT S.Title AS [Source Material], S.SourceMaterialEdition AS [Edition], CONCAT(A.FirstName, ' ', A.LastName) AS [Author], S.SourceMaterialType AS [Type], U.Nametag AS [username]
FROM KnowledgeBase.SourceMaterial S
INNER JOIN KnowledgeBase.SourceMaterialAuthor SA ON S.SourceMaterialID = SA.SourceMaterialID
INNER JOIN KnowledgeBase.Author A ON A.AuthorID = SA.AuthorID
LEFT OUTER JOIN
KnowledgeBase.UserProfile U
ON S.UserProfileID = U.UserProfileID

-- Test query: searching for excerpt card based on keyword

SELECT E.Title, E.Excerpt, K.Name AS [Keyword], U.Nametag AS [username]
FROM KnowledgeBase.ExcerptCard E
INNER JOIN KnowledgeBase.ExcerptCardKeyword EK ON E.ExcerptCardID = EK.ExcerptCardID
INNER JOIN KnowledgeBase.Keyword K ON EK.KeywordID = K.KeywordID
INNER JOIN KnowledgeBase.UserProfile U ON E.UserProfileID = U.UserProfileID
WHERE K.Name LIKE '%evil%';

-- Searching for keyword

SELECT N.Title, N.Body, K.Name AS [Keyword], U.Nametag AS [username]
FROM KnowledgeBase.Note N
INNER JOIN KnowledgeBase.NoteKeyword NK ON N.NoteID = NK.NoteID
INNER JOIN KnowledgeBase.Keyword K ON NK.KeywordID = K.KeywordID
INNER JOIN KnowledgeBase.UserProfile U ON N.UserProfileID = U.UserProfileID
--WHERE NK.KeywordID = 5;