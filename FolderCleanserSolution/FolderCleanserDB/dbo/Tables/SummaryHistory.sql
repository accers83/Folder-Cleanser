CREATE TABLE [dbo].[SummaryHistory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PathId] INT NOT NULL, 
    [ProcessingStartDateTime] DATETIME2 NOT NULL, 
    [ProcessingEndDateTime] DATETIME2 NOT NULL, 
    [ProcessingDurationMins] INT NOT NULL DEFAULT 0, 
    [FilesDeletedCount] INT NOT NULL DEFAULT 0, 
    [FileSizeDeletedBytes] INT NOT NULL DEFAULT 0
)
