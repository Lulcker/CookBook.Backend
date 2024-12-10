using CookBook.Backend.App.Commands.FavoriteRecipes;
using CookBook.Backend.App.Queries.FavoriteRecipes;
using CookBook.Backend.App.Queries.FavoriteRecipes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Backend.Controllers;

/// <summary>
/// Контроллер избранных рецептов
/// </summary>
[Authorize]
[ApiController]
[Route("api/favorite-recipe")]
public class FavoriteRecipeController(
    GetFavoriteRecipesQuery getFavoriteRecipesQuery,
    GetFavoriteRecipesCountQuery getFavoriteRecipesCountQuery,
    AddFavoriteRecipeCommand addFavoriteRecipeCommand,
    DeleteFavoriteRecipeCommand deleteFavoriteRecipeCommand
    ) : Controller
{
    #region GET

    /// <summary>
    /// Получение всех рецептов в избранном
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Список рецептов</returns>
    [HttpGet("all")]
    public async Task<IActionResult> All([FromQuery] GetFavoriteRecipesInputModel inputModel)
    {
        return Ok(await getFavoriteRecipesQuery.Execute(inputModel));
    }
    
    /// <summary>
    /// Получение количества избранных рецептов (для авторизованного пользователя)
    /// </summary>
    /// <returns>Количество избранных рецептов</returns>
    [HttpGet("count")]
    public async Task<IActionResult> Count()
    {
        return Ok(await getFavoriteRecipesCountQuery.Execute());
    }

    #endregion

    #region POST

    /// <summary>
    /// Добавление рецепта в избранное
    /// </summary>
    /// <param name="recipeId">Id рецепта</param>
    /// <returns>Результат операции</returns>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] Guid recipeId)
    {
        await addFavoriteRecipeCommand.Execute(recipeId);
        return Ok();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Удаление рецепта из избранного
    /// </summary>
    /// <param name="recipeId">Id рецепта</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{recipeId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid recipeId)
    {
        await deleteFavoriteRecipeCommand.Execute(recipeId);
        return Ok();
    }

    #endregion
}