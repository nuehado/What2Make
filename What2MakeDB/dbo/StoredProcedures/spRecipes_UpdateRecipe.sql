CREATE PROCEDURE [dbo].[spRecipes_UpdateRecipe]
	@Id int,
	@RecipeName nvarchar(50),
	@Description nvarchar(200),
	@Instructions nvarchar(2000)
AS
begin

	set nocount on;

	update dbo.Recipes
	set RecipeName = @RecipeName,
	[Description] = @Description,
	Instructions = @Instructions
	where Id = @Id

end
