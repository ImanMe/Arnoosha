using Arnoosha.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Arnoosha.Infrastructure.Data;

namespace Arnoosha.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Products.ToListAsync();
            if (result == null) return NotFound("No products available");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.Products.FindAsync(id);
            if (result == null) return NotFound("No product with that id");
            return Ok(result);
        }
    }
}
