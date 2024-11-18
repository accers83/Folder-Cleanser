CREATE PROCEDURE [dbo].[spPath_DeleteById]
	@Id int
AS
begin

	set nocount on;

	update dbo.[Path]
	set Deleted = getdate()
	where Id = @Id

end