using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Arnoosha.Core.Interfaces;

namespace Arnoosha.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productRepository.GetProductsAsync();
            if (result == null) return NotFound("No products available");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productRepository.GetProductByIdAsync(id);
            if (result == null) return NotFound("No product with that id");
            return Ok(result);
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
