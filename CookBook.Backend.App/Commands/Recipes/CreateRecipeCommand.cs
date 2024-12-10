using CookBook.Backend.App.Commands.Recipes.Models;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Recipes;

/// <summary>
/// Команда создания рецепта
/// </summary>
public class CreateRecipeCommand(
    IRepository<Recipe> recipeRepository,
    IRepository<Category> categoryRepository,
    IRepository<Product> productRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<CreateRecipeCommand> logger)
{
    public async Task Execute(CreateRecipeInputModel inputModel)
    {
        accessRightProvider.CheckIsAuthorized();
        
        InputModelHelper.TrimStringProperties(inputModel);

        if (string.IsNullOrWhiteSpace(inputModel.Name))
            throw new BusinessException("Название рецепта не может быть пустым");
        
        if (string.IsNullOrWhiteSpace(inputModel.Description))
            throw new BusinessException("Описание рецепта не может быть пустым");
        
        var category = await categoryRepository
            .FirstOrDefaultAsync(c => c.Id == inputModel.CategoryId);

        if (category is null)
            throw new BusinessException("Категория не найдена");

        foreach (var productQuantity in inputModel.Ingredients)
        {
            var product = await productRepository
                .FirstOrDefaultAsync(p => p.Id == productQuantity.ProductId);
            
            if (product is null)
                throw new BusinessException("Продукт не найден");
        }

        foreach (var recipeStep in inputModel.RecipeSteps)
        {
            InputModelHelper.TrimStringProperties(recipeStep);

            if (string.IsNullOrWhiteSpace(recipeStep.Description))
                throw new BusinessException("Шаг рецепта не может быть пустым");
        }

        var recipe = new Recipe
        {
            Name = inputModel.Name,
            Description = inputModel.Description,
            CreatedDateTime = DateTime.UtcNow,
            RecipeStatus = RecipeStatus.SendToModeration,
            PhotoLink = inputModel.PhotoLink,
            CategoryId = inputModel.CategoryId,
            UserId = userInfoProvider.Id!.Value
        };

        recipe.Ingredients = inputModel.Ingredients
            .Select(pq => new Ingredient
            {
                Quantity = pq.Quantity,
                UnitOfMeasure = pq.UnitOfMeasure,
                ProductId = pq.ProductId,
                RecipeId = recipe.Id
            }).ToList();

        recipe.RecipeSteps = inputModel.RecipeSteps
            .Select(rs => new RecipeStep
            {
                Description = rs.Description,
                PhotoLink = rs.PhotoLink,
                RecipeId = recipe.Id
            }).ToList();

        await recipeRepository.AddAsync(recipe);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Создан и отправлен на модерацию новый рецепт {RecipeName} от пользователя {Login}",
            recipe.Name, userInfoProvider.Login);
    }
}