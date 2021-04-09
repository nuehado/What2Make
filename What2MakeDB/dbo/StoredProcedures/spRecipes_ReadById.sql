CREATE PROCEDURE [dbo].[spRecipes_ReadById]
	@Id int
AS
begin

	set nocount on;

	SELECT [Id], [RecipeName], [Description], [Instructions]
	from dbo.Recipes
	where Id = @Id;

end
