﻿CREATE PROCEDURE [dbo].[spSummaryHistory_Get]

AS
begin

	set nocount on;

	select [s].[Id]
		,[s].[PathId]
		,[s].[ProcessingStartDateTime]
		,[s].[ProcessingEndDateTime]
		,[s].[ProcessingDurationMins]
		,[s].[FilesDeletedCount]
		,[s].[FileSizeDeletedMB]

	from dbo.SummaryHistory as s

		inner join dbo.[Path] as p on s.PathId = p.Id
									and p.Deleted is null

end