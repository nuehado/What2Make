CREATE PROCEDURE [dbo].[spRecipies_ReadById]
	@Id int
AS
begin

	set nocount on;

	SELECT [Id], [RecipeName], [Description], [Instructions]
	from dbo.Recipies
	where Id = @Id;

end
