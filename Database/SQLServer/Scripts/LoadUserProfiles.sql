USE KnowledgeBase;
GO

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

SELECT * FROM KnowledgeBase.UserProfile;
GO