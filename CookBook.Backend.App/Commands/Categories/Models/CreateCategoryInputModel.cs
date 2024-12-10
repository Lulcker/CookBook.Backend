namespace CookBook.Backend.App.Commands.Categories.Models;

/// <summary>
/// Входная модель для создания категории
/// </summary>
public class CreateCategoryInputModel
{
    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Описание категории
    /// </summary>
    public required string Description { get; set; }
}