/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select * from dbo.Recipies)
begin
    insert into dbo.Recipies(RecipeName, [Description], Instructions)
    values ('Hot Dog', 'An American ballpark classic for the whole family', 'Cook your hot dog franks in a boiling pot of water for 3 minutes and strain. Assembly frank in bun and top with condiments as desired');
    set @recipeId = LAST_INSERT_ID();
    
    insert into dbo.Ingredients (IngredientName)
    values ('hot dog');
    set @ingredientId = LAST_INSERT_ID();

    insert into dbo.RecipiesIngredients (RecipeId, IngredientId, Quantity)
    values (@recipeId, @ingredientId, '1 frank');

    insert into dbo.Ingredients (IngredientName)
    values ('hot dog bun');
    set @ingredientId = LAST_INSERT_ID();

    insert into dbo.RecipiesIngredients (RecipeId, IngredientId, Quantity)
    values (@recipeId, @ingredientId, '1 bun');

    insert into dbo.Ingredients (IngredientName)
    values ('ketchup');
    set @ingredientId = LAST_INSERT_ID();

    insert into dbo.RecipiesIngredients (RecipeId, IngredientId, Quantity)
    values (@recipeId, @ingredientId, 'optional');

    insert into dbo.Ingredients (IngredientName)
    values ('mustard');
    set @ingredientId = LAST_INSERT_ID();

    insert into dbo.RecipiesIngredients (RecipeId, IngredientId, Quantity)
    values (@recipeId, @ingredientId, 'optional');



    insert into dbo.Recipies(RecipeName, [Description], Instructions)
    values ('Simple Salad', 'Refreshingly light and easy greens to pair with any meal', 'Chop cherry tomatoes into halves. Julienne carrots into matchsticks. Break lettuce into individual leaves. Toss greens, tomatoes, and carrots together in a serving bowl. Add oil and vinegar to bowl, mix, and serve.');
    set @recipeId = LAST_INSERT_ID();
    
    insert into dbo.Ingredients (IngredientName)
    values ('romain lettuce');
    set @ingredientId = LAST_INSERT_ID();

    insert into dbo.RecipiesIngredients (RecipeId, IngredientId, Quantity)
    values (@recipeId, @ingredientId, '1 head');

    insert into dbo.Ingredients (IngredientName)
    values ('cherry tomatoes');
    set @ingredientId = LAST_INSERT_ID();

    insert into dbo.RecipiesIngredients (RecipeId, IngredientId, Quantity)
    values (@recipeId, @ingredientId, '10 - 20');

    insert into dbo.Ingredients (IngredientName)
    values ('carrots');
    set @ingredientId = LAST_INSERT_ID();

    insert into dbo.RecipiesIngredients (RecipeId, IngredientId, Quantity)
    values (@recipeId, @ingredientId, '2');

    insert into dbo.Ingredients (IngredientName)
    values ('oil');
    set @ingredientId = LAST_INSERT_ID();

    insert into dbo.RecipiesIngredients (RecipeId, IngredientId, Quantity)
    values (@recipeId, @ingredientId, '2 Tbsp');

    insert into dbo.Ingredients (IngredientName)
    values ('balsamic vinegar');
    set @ingredientId = LAST_INSERT_ID();

    insert into dbo.RecipiesIngredients (RecipeId, IngredientId, Quantity)
    values (@recipeId, @ingredientId, '1 Tbsp');
   
end

