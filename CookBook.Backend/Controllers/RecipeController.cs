using CookBook.Backend.App.Commands.Recipes;
using CookBook.Backend.App.Commands.Recipes.Models;
using CookBook.Backend.App.Queries.Recipes;
using CookBook.Backend.App.Queries.Recipes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Backend.Controllers;

/// <summary>
/// Контроллер рецептов
/// </summary>
[Authorize]
[ApiController]
[Route("api/recipe")]
public class RecipeController(
    GetRecipesQuery getRecipesQuery,
    GetSendToModerationRecipesQuery getSendToModerationRecipesQuery,
    GetRecipesByUserQuery getRecipesByUserQuery,
    GetRecipesByIdQuery getRecipesByIdQuery,
    CreateRecipeCommand createRecipeCommand,
    ApproveRecipeCommand approveRecipeCommand,
    RejectRecipeCommand rejectRecipeCommand,
    DeleteRecipeCommand deleteRecipeCommand
    ) : Controller
{
    #region GET

    /// <summary>
    /// Получение списка рецептов
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Список рецептов</returns>
    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<IActionResult> All([FromQuery] GetRecipesInputModel inputModel)
    {
        return Ok(await getRecipesQuery.Execute(inputModel));
    }
    
    /// <summary>
    /// Получение списка рецептов для модерации
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Список рецептов</returns>
    [HttpGet("all-moderation")]
    public async Task<IActionResult> AllModeration([FromQuery] GetAllRecipesForAdministratorInputModel inputModel)
    {
        return Ok(await getSendToModerationRecipesQuery.Execute(inputModel));
    }
    
    /// <summary>
    /// Получение списка рецептов для пользователя
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Список рецептов</returns>
    [HttpGet("all-by-user")]
    public async Task<IActionResult> AllByUser([FromQuery] GetAllRecipesByUserInputModel inputModel)
    {
        return Ok(await getRecipesByUserQuery.Execute(inputModel));
    }
    
    /// <summary>
    /// Получение рецепта
    /// </summary>
    /// <param name="recipeId">Id рецепта</param>
    /// <returns>Рецепт</returns>
    [AllowAnonymous]
    [HttpGet("{recipeId:guid}")]
    public async Task<IActionResult> ById([FromRoute] Guid recipeId)
    {
        return Ok(await getRecipesByIdQuery.Execute(recipeId));
    }

    #endregion

    #region POST

    /// <summary>
    /// Создание и отправка на модерацию нового рецепта
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRecipeInputModel inputModel)
    {
        await createRecipeCommand.Execute(inputModel);
        return Ok();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Одобрение рецепта
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [HttpPatch("approve")]
    public async Task<IActionResult> Approve([FromBody] ApproveRecipeInputModel inputModel)
    {
        await approveRecipeCommand.Execute(inputModel);
        return Ok();
    }
    
    /// <summary>
    /// Отклонение рецепта
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [HttpPatch("reject")]
    public async Task<IActionResult> Reject([FromBody] RejectRecipeInputModel inputModel)
    {
        await rejectRecipeCommand.Execute(inputModel);
        return Ok();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Удаление рецепта
    /// </summary>
    /// <param name="recipeId">Id рецепта</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{recipeId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid recipeId)
    {
        await deleteRecipeCommand.Execute(recipeId);
        return Ok();
    }

    #endregion
}