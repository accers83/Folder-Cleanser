CREATE PROCEDURE [dbo].[spSummaryHistory_GetByPathId]
	@PathId int
AS
begin

	set nocount on;

	select [Id]
		,[PathId]
		,[ProcessingStartDateTime]
		,[ProcessingEndDateTime]
		,[ProcessingDurationMins]
		,[FilesDeletedCount]
		,[FileSizeDeletedBytes]
	
	from dbo.SummaryHistory
	where PathId = @PathId

end
