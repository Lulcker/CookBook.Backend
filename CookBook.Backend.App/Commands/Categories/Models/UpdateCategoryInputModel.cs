namespace CookBook.Backend.App.Commands.Categories.Models;

/// <summary>
/// Входная модель для обновления категории
/// </summary>
public class UpdateCategoryInputModel
{
    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Описание категории
    /// </summary>
    public required string Description { get; set; }
}