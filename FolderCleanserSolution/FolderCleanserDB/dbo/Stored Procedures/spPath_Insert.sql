CREATE PROCEDURE [dbo].[spPath_Insert]
	@Path nvarchar(500),
	@RetentionDays int
AS
begin

	set nocount on;

	if not exists (select 1 from dbo.[Path]
					where [Path] = @Path 
						and RetentionDays = @RetentionDays 
						and Deleted is null
				  )
	begin

		insert into dbo.[Path] ([Path], RetentionDays)
		values (@Path, @RetentionDays)
	end

end
