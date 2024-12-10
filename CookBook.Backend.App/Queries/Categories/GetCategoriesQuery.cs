using AutoMapper;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.App.Queries.Categories.Models;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.App.Queries.Categories;

/// <summary>
/// Запрос для получения категорий
/// </summary>
public class GetCategoriesQuery(
    IRepository<Category> categoryRepository,
    IMapper mapper)
{
    public async Task<IReadOnlyCollection<CategoryModel>> Execute(GetCategoriesInputModel inputModel)
    {
        var categories = categoryRepository
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(inputModel.Search))
            categories = categories
                .Where(c => c.Name.ToLower()
                    .Contains(inputModel.Search));
        
        categories = categories.OrderByCategory(inputModel.OrderBy);
        
        categories = categories
            .Skip(ConstHelper.PerPage * inputModel.Page)
            .Take(ConstHelper.PerPage);

        return await mapper
            .ProjectTo<CategoryModel>(categories)
            .ToListAsync();
    }
}