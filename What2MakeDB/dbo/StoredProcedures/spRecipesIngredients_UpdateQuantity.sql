CREATE PROCEDURE [dbo].[spRecipesIngredients_UpdateQuantity]
	@RecipeId int,
	@IngredientId int,
	@Quantity nvarchar(10)
AS
begin
	
	set nocount on;

	update dbo.RecipesIngredients
	set Quantity = @Quantity
	where RecipeId = @RecipeId and IngredientId = @IngredientId

end
