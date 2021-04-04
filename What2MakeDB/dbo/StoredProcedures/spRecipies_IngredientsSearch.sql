CREATE PROCEDURE [dbo].[spRecipies_IngredientsSearch]
	@Ingredient1 nvarchar(50),
	@Ingredient2 nvarchar(50),
	@Ingredient3 nvarchar(50),
	@Ingredient4 nvarchar(50),
	@Ingredient5 nvarchar(50)
AS
begin
	set nocount on;

	with t as(
	select r.Id, r.RecipeName, [Description], ROW_NUMBER() over(partition by RecipeName order by RecipeName) as Matches 
	from dbo.Recipies r
		inner join dbo.RecipiesIngredients ri on ri.RecipeId = r.Id
		inner join dbo.Ingredients i on i.Id = ri.IngredientId
		where i.IngredientName like '%'+@Ingredient1+'%'
		or i.IngredientName like '%'+@Ingredient2+'%'
		or i.IngredientName like '%'+@Ingredient3+'%'
		or i.IngredientName like '%'+@Ingredient4+'%'
		or i.IngredientName like '%'+@Ingredient5+'%'
	)

	select t.Id, t.RecipeName, t.[Description], t.Matches
	from t
		left outer join t [copy]
		on t.RecipeName = [copy].RecipeName and t.Matches < [copy].Matches
		where [copy].RecipeName is null
	order by Matches desc
end
