CREATE PROCEDURE [dbo].[spRecipes_CreateNew]
	@RecipeName nvarchar(50),
	@Description nvarchar(200),
	@Instructions nvarchar(2000),
	@Id int output
AS
begin
	set nocount on;

	if not exists (select 1 from dbo.Recipes where RecipeName = @RecipeName)
	begin
		insert into dbo.Recipes(RecipeName, [Description], Instructions)
		values (@RecipeName, @Description, @Instructions);

		select top 1 @Id = Id
		from dbo.Recipes
		where RecipeName = @RecipeName;
	end

	else
	begin
		select @Id = 0;
	end

end
