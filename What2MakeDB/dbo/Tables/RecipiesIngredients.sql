CREATE TABLE [dbo].[RecipiesIngredients]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [RecipeId] INT NOT NULL, 
    [IngredientId] INT NOT NULL, 
    [Quantity] NVARCHAR(10) NOT NULL
    CONSTRAINT [FK_RecipiesIngredients_Recipies] FOREIGN KEY (RecipeId) REFERENCES Recipies(Id)
    CONSTRAINT [FK_RecipiesIngredients_Ingredients] FOREIGN KEY (IngredientId) REFERENCES Ingredients(Id)
)
