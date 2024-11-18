CREATE PROCEDURE [dbo].[SummaryHistory_Insert]
	@PathId int,
	@ProcessingStartDateTime datetime2(7),
	@ProcessingEndDateTime datetime2(7),
	@ProcessingDurationMins int,
	@FilesDeletedCount int,
	@FileSizeDeletedBytes int
AS
begin

	set nocount on;

	insert into dbo.SummaryHistory (PathId, ProcessingStartDateTime, ProcessingEndDateTime,
									ProcessingDurationMins, FilesDeletedCount, FileSizeDeletedBytes)
	values (@PathId, @ProcessingStartDateTime, @ProcessingEndDateTime,
			@ProcessingDurationMins, @FilesDeletedCount, @FileSizeDeletedBytes)

end
