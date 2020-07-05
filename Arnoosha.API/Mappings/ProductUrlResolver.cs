using Arnoosha.API.Dtos;
using Arnoosha.Core.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Arnoosha.API.Mappings
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
                return _config["ApiUrl"] + source.PictureUrl;

            return null;
        }
    }
}
