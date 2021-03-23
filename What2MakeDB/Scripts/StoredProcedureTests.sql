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