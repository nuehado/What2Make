CREATE PROCEDURE [dbo].[spIngredients_GetByRecipeId]
	@RecipeId int
AS
begin
	set nocount on

	select i.[Id], [IngredientName], [Quantity]
	from dbo.Ingredients i
	inner join dbo.RecipesIngredients ri on RecipeId = @RecipeId
	where i.Id = ri.IngredientId;

end
