CREATE TABLE [dbo].[Recipies]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [RecipeName] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(200) NOT NULL, 
    [Instructions] NVARCHAR(2000) NOT NULL
)
