CREATE PROCEDURE [dbo].[spSummaryHistory_Insert]
	@PathId int,
	@ProcessingStartDateTime datetime2(7),
	@ProcessingEndDateTime datetime2(7),
	@ProcessingDurationMins int,
	@FilesDeletedCount int,
	@FileSizeDeletedMB float
AS
begin

	set nocount on;

	insert into dbo.SummaryHistory (PathId, ProcessingStartDateTime, ProcessingEndDateTime,
									ProcessingDurationMins, FilesDeletedCount, FileSizeDeletedMB)
	values (@PathId, @ProcessingStartDateTime, @ProcessingEndDateTime,
			@ProcessingDurationMins, @FilesDeletedCount, @FileSizeDeletedMB)

end
