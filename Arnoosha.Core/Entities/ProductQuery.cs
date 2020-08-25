﻿using Arnoosha.Core.Interfaces;

namespace Arnoosha.Core.Entities
{
    public class ProductQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; } = 1;
        public byte PageSize { get; set; } = 5;
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }
    }
}
