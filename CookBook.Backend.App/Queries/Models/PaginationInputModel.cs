namespace CookBook.Backend.App.Queries.Models;

/// <summary>
/// Входная модель пагинации
/// </summary>
public class PaginationInputModel
{
    /// <summary>
    /// Страница
    /// </summary>
    public required int Page { get; set; }

    /// <summary>
    /// Поиск
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// Сортировка
    /// </summary>
    public string? OrderBy { get; set; }
}