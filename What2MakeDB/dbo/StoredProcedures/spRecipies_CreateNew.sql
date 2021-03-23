CREATE PROCEDURE [dbo].[spRecipies_CreateNew]
	@RecipeName nvarchar(50),
	@Description nvarchar(200),
	@Instructions nvarchar(2000),
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.Recipies(RecipeName, [Description], Instructions)
	values (@RecipeName, @Description, @Instructions)

	set @Id = SCOPE_IDENTITY();

end
