using System;
using Arnoosha.Core.Entities;
using Arnoosha.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Arnoosha.Infrastructure.Data.Extensions;
using Arnoosha.Infrastructure.Data.Helpers;

namespace Arnoosha.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        private readonly Dictionary<string, Expression<Func<Product, object>>> _sortColumnsMap;
        public ProductRepository(StoreContext context)
        {
            _context = context;
            _sortColumnsMap = ColumnsMap.CreateSortColumnsMap();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p=> p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductQuery queryObj)
        {
            var query =  _context.Products
                .Include(p=>p.ProductType)
                .Include(p => p.ProductBrand)
                    .AsQueryable();

            var filterColumnsMap = ColumnsMap.CreateFilterColumnsMap(queryObj);

            query = query.ApplyFiltering(queryObj, filterColumnsMap);

            query = query.ApplyOrdering(queryObj, _sortColumnsMap);

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
