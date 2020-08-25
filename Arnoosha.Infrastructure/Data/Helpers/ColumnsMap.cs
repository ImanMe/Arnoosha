using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Arnoosha.Core.Entities;

namespace Arnoosha.Infrastructure.Data.Helpers
{
    public static class ColumnsMap
    {
        public static Dictionary<string, Expression<Func<Product, object>>> CreateSortColumnsMap()
        {
            return new Dictionary<string, Expression<Func<Product, object>>>
            {
                ["name"] = v => v.Name,
                ["price"] = v => (double?)v.Price,
            };
        }
    }
}
