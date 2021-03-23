CREATE PROCEDURE [dbo].[spRecipies_CreateNew]
	@RecipeName nvarchar(50),
	@Description nvarchar(200),
	@Instructions nvarchar(2000),
	@Id int output
AS
begin
	set nocount on;

	if not exists (select 1 from dbo.Recipies where RecipeName = @RecipeName)
	begin
		insert into dbo.Recipies(RecipeName, [Description], Instructions)
		values (@RecipeName, @Description, @Instructions);
	end
	
	select top 1 @Id = Id
	from dbo.Recipies
	where RecipeName = @RecipeName;
	

end
