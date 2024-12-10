namespace CookBook.Backend.App.Queries.Categories.Models;

/// <summary>
/// Модель категории
/// </summary>
public class CategoryModel
{
    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }
}