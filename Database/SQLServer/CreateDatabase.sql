-- Create Database 
USE master;
GO

CREATE DATABASE KnowledgeBase;
GO

-- Create Schema for KnowledgeBase

USE KnowledgeBase;
GO 

CREATE SCHEMA KnowledgeBase
    AUTHORIZATION dbo;
GO

-- Create filegroups


-------------------!!!!!! SET BOTH FILES TO SAME DRIVE IF HOST SYSTEM ONLY HAS ONE DRIVE !!!!!!-----------------------------
ALTER DATABASE KnowledgeBase
    ADD FILEGROUP KnowledgeBase_dat;
ALTER DATABASE KnowledgeBase
    ADD FILE (
        NAME = KnowledgeBase_dat,
        FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\knowledgebase_dat.mdf'
    )
    TO FILEGROUP KnowledgeBase_dat;

ALTER DATABASE KnowledgeBase
    ADD LOG FILE (
        NAME = KnowledgeBase_log,
        -- Pick one filename, if system only has one drive, choose c drive, otherwise choose D or define the drive you will use.
        FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\knowledgebase_log.ldf'
        -- FILENAME = 'D:\SQL\SQLEXPRESS\KnowlegeBase\LOGS\KnowledgeBase_log.ldf'
    );

GO