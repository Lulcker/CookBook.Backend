namespace CookBook.Backend.App.Commands.Products.Models;

/// <summary>
/// Входная модель для создания продукта
/// </summary>
public class CreateProductInputModel
{
    /// <summary>
    /// Название продукта
    /// </summary>
    public required string Name { get; set; }
}