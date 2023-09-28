USE KnowledgeBase;
GO

--DROP TABLE IF EXISTS KnowledgeBase.SourceMaterial;

CREATE TABLE KnowledgeBase.SourceMaterial (
    SourceMaterialID INT IDENTITY (1,1) NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    PublishDate DATE NOT NULL,
    Publisher NVARCHAR(200) NULL,
    SourceMaterialType VARCHAR(50) NOT NULL,
    SourceMaterialEdition VARCHAR(30) NULL,
    UserProfileID INT NOT NULL,
    CONSTRAINT PK_SourceMaterial_SourceMaterialID PRIMARY KEY CLUSTERED (SourceMaterialID ASC),
    CONSTRAINT AK_SourceMaterial_Title_SourceMaterialEdition UNIQUE NONCLUSTERED (Title, SourceMaterialEdition),
    CONSTRAINT FK_SourceMaterial_UserProfileID_UserProfile_UserProfileID
        FOREIGN KEY (UserProfileID) REFERENCES KnowledgeBase.UserProfile(UserProfileID)
        ON DELETE CASCADE
)
ON KnowledgeBase_dat
WITH (DATA_COMPRESSION = PAGE);
GO