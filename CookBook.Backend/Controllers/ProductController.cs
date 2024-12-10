using CookBook.Backend.App.Commands.Products;
using CookBook.Backend.App.Commands.Products.Models;
using CookBook.Backend.App.Queries.Products;
using CookBook.Backend.App.Queries.Products.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Backend.Controllers;

/// <summary>
/// Контроллер продуктов
/// </summary>
[Authorize]
[ApiController]
[Route("api/product")]
public class ProductController(
    GetProductsQuery getProductsQuery,
    CreateProductCommand createProductCommand,
    UpdateProductCommand updateProductCommand,
    DeleteProductCommand deleteProductCommand
    ) : Controller
{
    #region GET

    /// <summary>
    /// Список всех продуктов
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Список продуктов</returns>
    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<IActionResult> All([FromQuery] GetProductsInputModel inputModel)
    {
        return Ok(await getProductsQuery.Execute(inputModel));
    }

    #endregion

    #region POST

    /// <summary>
    /// Создание продукта
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateProductInputModel inputModel)
    {
        await createProductCommand.Execute(inputModel);
        return Ok();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Обновление продукта
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [HttpPatch("update")]
    public async Task<IActionResult> Update(UpdateProductInputModel inputModel)
    {
        await updateProductCommand.Execute(inputModel);
        return Ok();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Удаление продукта
    /// </summary>
    /// <param name="productId">Id продукта</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        await deleteProductCommand.Execute(productId);
        return Ok();
    }

    #endregion
}