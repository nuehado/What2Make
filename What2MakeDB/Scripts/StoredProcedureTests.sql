/*
/*test recipe creation*/
begin
	declare @RecipeName nvarchar(50) = 'test recipe';
	declare @Description nvarchar(200) = 'test description';
	declare @Instructions nvarchar(2000) = 'test instructions';
	declare @Id int;
	
	set nocount on;

	if not exists (select 1 from dbo.Recipies where RecipeName = @RecipeName)
	begin
		insert into dbo.Recipies(RecipeName, [Description], Instructions)
		values (@RecipeName, @Description, @Instructions);
	end
	
	select top 1 @Id = Id
	from dbo.Recipies
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
	
	insert into dbo.RecipiesIngredients(RecipeId, IngredientId, Quantity)
	values (@RecipeId, @Id, @Quantity);

end

/*test searching by ingredient*/
begin
	
	declare @Ingredient1 nvarchar(50) = 'cherry tomatoes';
	declare @Ingredient2 nvarchar(50) = 'hot dog bun';
	declare @Ingredient3 nvarchar(50) = 'KETCHUP';
	declare @Ingredient4 nvarchar(50) = 'romain lettuce';
	declare @Ingredient5 nvarchar(50) = 'HOT doG';

	set nocount on;

	with t as(
	select r.Id, r.RecipeName, [Description], ROW_NUMBER() over(partition by RecipeName order by RecipeName) as Matches 
	from dbo.Recipies r
	inner join dbo.RecipiesIngredients ri on ri.RecipeId = r.Id
	inner join dbo.Ingredients i on i.Id = ri.IngredientId
	where i.IngredientName in (@Ingredient1, @Ingredient2, @Ingredient3, @Ingredient4, @Ingredient5)
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
	inner join dbo.RecipiesIngredients ri on RecipeId = @RecipeId
	where i.Id = ri.IngredientId;
end
*/