CREATE PROCEDURE [dbo].[spRecipies_ReadAll]
AS
begin
	
	set nocount on;

	select [Id], [RecipeName], [Description], [Instructions]
	from dbo.Recipies

end
