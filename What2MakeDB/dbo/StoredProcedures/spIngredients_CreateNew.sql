CREATE PROCEDURE [dbo].[spIngredients_CreateNew]
	@IngredientName nvarchar(50),
	@Quantity nvarchar(10),
	@RecipeId int,
	@Id int
AS
begin
	set nocount on;

	if not exists (select 1 from dbo.Ingredients where IngredientName = @IngredientName)
	begin
		insert into dbo.Ingredients(IngredientName)
		values (@IngredientName);
	end
	
	select top 1 @Id = Id
	from dbo.Ingredients
	where IngredientName = @IngredientName;
	
	insert into dbo.RecipiesIngredients(RecipeId, IngredientId, Quantity)
	values (@RecipeId, @Id, @Quantity);

end
