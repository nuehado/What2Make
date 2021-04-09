CREATE PROCEDURE [dbo].[spRecipes_GetAll]
AS
begin
	
	set nocount on;

	select [Id], [RecipeName], [Description], [Instructions]
	from dbo.Recipes

end
