namespace CookBook.Backend.Domain.Dictionaries;

/// <summary>
/// Статус рецепта
/// </summary>
public enum RecipeStatus
{
    /// <summary>
    /// Отправлен на модерацию
    /// </summary>
    SendToModeration,
    
    /// <summary>
    /// Опубликован
    /// </summary>
    Published,
    
    /// <summary>
    /// Отказ
    /// </summary>
    Reject
}