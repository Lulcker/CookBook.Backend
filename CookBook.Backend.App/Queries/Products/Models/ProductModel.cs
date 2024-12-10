namespace CookBook.Backend.App.Queries.Products.Models;

/// <summary>
/// Модель продукта
/// </summary>
public class ProductModel
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