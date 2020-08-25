using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Arnoosha.Core.Entities;
using Arnoosha.Core.Interfaces;

namespace Arnoosha.Infrastructure.Data.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, ProductQuery queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (string.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
                return query;

            return queryObj.IsSortAscending
                ? query.OrderBy(columnsMap[queryObj.SortBy])
                : query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, QueryObject queryObj)
        {
            return query
                .Skip((queryObj.PageIndex - 1) * queryObj.PageSize)
                .Take(queryObj.PageSize);
        }

        public static IQueryable<Product> ApplyProductFilterAndSearch(this IQueryable<Product> query, ProductQuery queryObj)
        {
            if (queryObj.TypeId != 0)
                query = query
                    .Where(x => x.ProductTypeId == queryObj.TypeId);

            if (queryObj.BrandId != 0)
                query = query.Where(x => x.ProductBrandId == queryObj.BrandId);

            if (!string.IsNullOrEmpty(queryObj.Search))
                query = query.Where(x => x.Name.ToLower()
                    .Contains(queryObj.Search));

            return query;
        }
    }
}
