using System.Collections.Generic;

namespace Arnoosha.Core.Entities
{
    public class QueryResult<T> where T :class
    {
        public QueryResult(int pageIndex, int pageSize, int count, IReadOnlyCollection<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyCollection<T> Data { get; set; }
    }
}
