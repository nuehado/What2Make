CREATE PROCEDURE [dbo].[spRecipiesIngredients_UpdateQuantity]
	@RecipeId int,
	@IngredientId int,
	@Quantity nvarchar(10)
AS
begin
	
	set nocount on;

	update dbo.RecipiesIngredients
	set Quantity = @Quantity
	where RecipeId = @RecipeId and IngredientId = @IngredientId

end
