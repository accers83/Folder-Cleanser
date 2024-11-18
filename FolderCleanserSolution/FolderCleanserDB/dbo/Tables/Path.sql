CREATE TABLE [dbo].[Path]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Path] NVARCHAR(500) NOT NULL, 
    [RetentionDays] INT NOT NULL, 
    [Created] DATETIME2 NOT NULL DEFAULT getdate(), 
    [Deleted] DATETIME2 NULL
)
