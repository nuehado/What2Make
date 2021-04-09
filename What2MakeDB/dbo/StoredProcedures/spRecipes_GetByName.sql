CREATE PROCEDURE [dbo].[spRecipes_GetByName]
	@RecipeName nvarchar(50)
AS
begin
	set nocount on;

	SELECT [Id], [RecipeName], [Description]
	from dbo.Recipes
	where RecipeName like '%'+@RecipeName+'%';

end
