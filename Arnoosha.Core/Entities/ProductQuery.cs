using Arnoosha.Core.Interfaces;

namespace Arnoosha.Core.Entities
{
    public class ProductQuery : QueryObject
    {
        public int TypeId { get; set; }
        public int BrandId { get; set; }
    }
}
