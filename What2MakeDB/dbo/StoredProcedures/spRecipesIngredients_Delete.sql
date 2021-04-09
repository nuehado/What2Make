CREATE PROCEDURE [dbo].[spRecipesIngredients_Delete]
	@RecipeId int = 0,
	@IngredientId int
AS
begin

	set nocount on

	delete
	from dbo.RecipesIngredients
	where RecipeId = @RecipeId and IngredientId = @IngredientId

	if not exists (select 1 from dbo.RecipesIngredients where IngredientId = @IngredientId)
	begin
		delete
		from dbo.Ingredients
		where Id = @IngredientId;
	end

end
