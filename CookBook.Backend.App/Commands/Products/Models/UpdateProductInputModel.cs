namespace CookBook.Backend.App.Commands.Products.Models;

/// <summary>
/// Входная модель для обновления продукта
/// </summary>
public class UpdateProductInputModel
{
    /// <summary>
    /// Id продукта
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Название продукта
    /// </summary>
    public required string Name { get; set; }
}