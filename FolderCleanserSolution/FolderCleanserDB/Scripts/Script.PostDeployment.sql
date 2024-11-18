/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select 1 from dbo.[Path])
begin

    insert into dbo.[Path] ([Path], [RetentionDays])
    values ('C:\temp\Folder-Cleanser-Dev\A', 1), ('C:\temp\Folder-Cleanser-Dev\B', 10) 

end