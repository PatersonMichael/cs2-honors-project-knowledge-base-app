USE KnowledgeBase;
GO

-- Before running this script you may want to run RecreateAllTables WARNING!!!!! This will delete all tables in the database, only do this if working on a dev/test version of the DB !!!!!!!!

-- UserProfiles --

INSERT INTO KnowledgeBase.UserProfile (
    FirstName,
    LastName,
    Email,
    CreationDate,
    BirthDate
)
VALUES
    ('John', 'Smith', 'JSmith@Sample.com', '20230908', '19980929'),
    ('Sarah', 'Smith', 'SSmith@Sample.com', '20230908', '19890924'),
    ('Gabe', 'Oliver', 'NotyourFriend27@gweegel.org', '20230908', '20000117'),
    ('Nickel', 'Beck', 'GrossGuy@bahoo.com', '20230908', '20130522');

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
    ('The Lord of the Rings: Fellowship of the Ring', '19540729', 'Houghton Mifflin Harcourt', 'Book', '1982', '3'),
    ('The Lord of the Rings: Fellowship of the Ring', '19540729', 'Houghton Mifflin Harcourt', 'Book', '1954', '2'),
    ('Ethics, the Fundamentals', '2007', 'Blackwell Publishing', 'Book', '2007', '2');

GO

SELECT * FROM KnowledgeBase.UserProfile;
SELECT * FROM KnowledgeBase.SourceMaterial;

-- Test query to see what users added each source material

SELECT S.Title AS [Source Material], S.SourceMaterialEdition, S.SourceMaterialType, CONCAT(U.FirstName, ' ',  U.LastName) AS [Username]
FROM KnowledgeBase.SourceMaterial S
LEFT OUTER JOIN
KnowledgeBase.UserProfile U
ON S.UserProfileID = U.UserProfileID