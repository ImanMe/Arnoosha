﻿using Arnoosha.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arnoosha.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<QueryResult<Product>> GetProductsAsync(ProductQuery query);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}
