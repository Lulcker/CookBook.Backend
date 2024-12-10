using CookBook.Backend.App.Commands.Categories;
using CookBook.Backend.App.Commands.Categories.Models;
using CookBook.Backend.App.Queries.Categories;
using CookBook.Backend.App.Queries.Categories.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Backend.Controllers;

/// <summary>
/// Контроллер категорий
/// </summary>
[Authorize]
[ApiController]
[Route("api/category")]
public class CategoryController(
    GetCategoriesQuery getCategoriesQuery,
    CreateCategoryCommand createCategoryCommand,
    UpdateCategoryCommand updateCategoryCommand,
    DeleteCategoryCommand deleteCategoryCommand
    ) : Controller
{
    #region GET

    /// <summary>
    /// Получение всех категорий
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Список категорий</returns>
    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<IActionResult> All([FromQuery] GetCategoriesInputModel inputModel)
    {
        return Ok(await getCategoriesQuery.Execute(inputModel));
    }

    #endregion

    #region POST

    /// <summary>
    /// Создание категории
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCategoryInputModel inputModel)
    {
        await createCategoryCommand.Execute(inputModel);
        return Ok();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Обновление категории
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [HttpPatch("update")]
    public async Task<IActionResult> Update(UpdateCategoryInputModel inputModel)
    {
        await updateCategoryCommand.Execute(inputModel);
        return Ok();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Удаление категории
    /// </summary>
    /// <param name="categoryId">Id категории</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> Delete(Guid categoryId)
    {
        await deleteCategoryCommand.Execute(categoryId);
        return Ok();
    }

    #endregion
}