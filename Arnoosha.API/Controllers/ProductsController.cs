using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Arnoosha.API.Dtos;
using Arnoosha.API.Errors;
using Arnoosha.Core.Entities;
using Arnoosha.Core.Interfaces;
using AutoMapper;

namespace Arnoosha.API.Controllers
{
    public class ProductsController : CustomControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery]ProductQuery queryObj)
        {
            var products = await _productRepository.GetProductsAsync(queryObj);

            if (!products.Any())
                return NotFound(new ApiResponse(404, "No product was found!"));
            
            var productsDto = 
                _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null) 
                return NotFound(new ApiResponse(404, "No product was found!"));
                
            var productDto = _mapper.Map<Product, ProductDto>(product);

            return Ok(productDto);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrands()
        {
            return Ok(await _productRepository.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
        {
            return Ok(await _productRepository.GetProductTypesAsync());
        }
    }
}
