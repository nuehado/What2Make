CREATE TABLE [dbo].[RecipesIngredients]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RecipeId] INT NOT NULL, 
    [IngredientId] INT NOT NULL, 
    [Quantity] NVARCHAR(10) NOT NULL
    CONSTRAINT [FK_RecipesIngredients_Recipes] FOREIGN KEY (RecipeId) REFERENCES Recipes(Id)
    CONSTRAINT [FK_RecipesIngredients_Ingredients] FOREIGN KEY (IngredientId) REFERENCES Ingredients(Id)
)
