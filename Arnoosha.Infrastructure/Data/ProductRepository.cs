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

        public async Task<QueryResult<Product>> GetProductsAsync(ProductQuery queryObj)
        {
            var query =  _context.Products
                .Include(p=>p.ProductType)
                .Include(p => p.ProductBrand)
                    .AsQueryable();

            query = query.ApplyProductFilterAndSearch(queryObj);

            var count  = await query.CountAsync();
            
            query = query.ApplyOrdering(queryObj, _sortColumnsMap);

            query = query.ApplyPaging(queryObj);

            var data = await query.ToListAsync();

            var result = new QueryResult<Product>(queryObj.PageIndex, queryObj.PageSize, count, data);

            return result;
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
