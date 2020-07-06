using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Arnoosha.Core.Entities;

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

        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, ProductQuery queryObj, Dictionary<string, Expression<Func<T, bool>>> columnsMap)
        {
            if (queryObj.ProductBrandId != 0 && columnsMap.ContainsKey("productBrandId"))
                query = query.Where(columnsMap["productBrandId"]);

            if (queryObj.ProductTypeId != 0 && columnsMap.ContainsKey("productTypeId"))
                query = query.Where(columnsMap["productTypeId"]);

            return query;
        }
    }
}
