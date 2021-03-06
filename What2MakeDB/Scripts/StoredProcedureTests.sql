/*
/*test recipe creation*/
begin
	declare @RecipeName nvarchar(50) = 'test recipe';
	declare @Description nvarchar(200) = 'test description';
	declare @Instructions nvarchar(2000) = 'test instructions';
	declare @Id int;
	
	set nocount on;

	if not exists (select 1 from dbo.Recipes where RecipeName = @RecipeName)
	begin
		insert into dbo.Recipes(RecipeName, [Description], Instructions)
		values (@RecipeName, @Description, @Instructions);
	end
	
	select top 1 @Id = Id
	from dbo.Recipes
	where RecipeName = @RecipeName;
	
	print @Id

end

/*test ingredient insert and junction*/
begin
	
	declare @IngredientName nvarchar(50) = 'relish';
	declare @Quantity nvarchar(10) = 'optional';
	declare @RecipeId int = 13;
	declare @Id int;
	set nocount on;

	if not exists (select 1 from dbo.Ingredients where IngredientName = @IngredientName)
	begin
		insert into dbo.Ingredients(IngredientName)
		values (@IngredientName);
	end
	
	select top 1 @Id = Id
	from dbo.Ingredients
	where IngredientName = @IngredientName;
	
	insert into dbo.RecipesIngredients(RecipeId, IngredientId, Quantity)
	values (@RecipeId, @Id, @Quantity);

end

/*test searching by ingredient*/
begin
	
	declare @Ingredient1 nvarchar(50) = 'oil';
	declare @Ingredient2 nvarchar(50) = 'hot dog bun';
	declare @Ingredient3 nvarchar(50) = 'KETCHUP';
	declare @Ingredient4 nvarchar(50) = 'rom';
	declare @Ingredient5 nvarchar(50) = 'doG';

	set nocount on;

	with t as(
	select r.Id, r.RecipeName, [Description], ROW_NUMBER() over(partition by RecipeName order by RecipeName) as Matches 
	from dbo.Recipes r
		inner join dbo.RecipesIngredients ri on ri.RecipeId = r.Id
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

/*test getting all ingredients for one recipe*/
begin
	declare @RecipeId int = 14

	set nocount on

	select i.[Id], [IngredientName], [Quantity]
	from dbo.Ingredients i
	inner join dbo.RecipesIngredients ri on RecipeId = @RecipeId
	where i.Id = ri.IngredientId;
end
*/

/*test for updating recipe information
begin
	declare @Id int = 7015;
	declare @RecipeName nvarchar(50) = 'updated name';
	declare @Description nvarchar(200) = 'updated description';
	declare @Instructions nvarchar(2000) = 'updated instructions';
	set nocount on;

	update dbo.Recipes
	set RecipeName = @RecipeName,
	[Description] = @Description,
	Instructions = @Instructions
	where Id = @Id

end
*/

/*test for updating ingredient quantity for recipe
begin
	declare @RecipeId int = 7015;
	declare @IngredientId int = 6064;
	declare @Quantity nvarchar(10) = 'updated q';

	set nocount on;

	update dbo.RecipesIngredients
	set Quantity = @Quantity
	where RecipeId = @RecipeId and IngredientId = @IngredientId

end

*/