CREATE PROCEDURE [dbo].[spIngredients_CreateNew]
	@IngredientName nvarchar(50),
	@Quantity nvarchar(10),
	@RecipeId int
AS
begin
	
	set nocount on;

	if not exists (select 1 from dbo.Ingredients where IngredientName = @IngredientName)
	begin
		insert into dbo.Ingredients(IngredientName)
		values (@IngredientName);
	end
	
	declare @IngredientId int;
	select top 1 @IngredientId = Id
	from dbo.Ingredients
	where IngredientName = @IngredientName;
	
	if not exists (select 1 from dbo.RecipesIngredients where RecipeId = @RecipeId and IngredientId = @IngredientId)
	begin
	insert into dbo.RecipesIngredients(RecipeId, IngredientId, Quantity)
	values (@RecipeId, @IngredientId, @Quantity);
	end

end
