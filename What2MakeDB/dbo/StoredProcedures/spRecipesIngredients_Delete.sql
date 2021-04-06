CREATE PROCEDURE [dbo].[spRecipesIngredients_Delete]
	@RecipeId int = 0,
	@IngredientId int
AS
begin

	set nocount on

	delete
	from dbo.RecipiesIngredients
	where RecipeId = @RecipeId and IngredientId = @IngredientId
end
