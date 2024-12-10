using AutoMapper;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.App.Queries.Products.Models;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.App.Queries.Products;

/// <summary>
/// Запрос для получения списка продуктов
/// </summary>
public class GetProductsQuery(
    IRepository<Product> productRepository,
    IMapper mapper)
{
    public async Task<IReadOnlyCollection<ProductModel>> Execute(GetProductsInputModel inputModel)
    {
        var products = productRepository
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(inputModel.Search))
            products = products
                .Where(p => p.Name.ToLower()
                    .Contains(inputModel.Search.ToLower()));
        
        products = products.OrderByProduct(inputModel.OrderBy);

        products = products
            .Skip(ConstHelper.PerPage * inputModel.Page)
            .Take(ConstHelper.PerPage);

        return await mapper
            .ProjectTo<ProductModel>(products)
            .ToListAsync();
    }
}