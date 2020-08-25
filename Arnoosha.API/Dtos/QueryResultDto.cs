using System.Collections.Generic;

namespace Arnoosha.API.Dtos
{
    public class QueryResultDto<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyCollection<T> Data { get; set; }
    }
}
