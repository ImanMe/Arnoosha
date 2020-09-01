using System.Threading.Tasks;
using Arnoosha.Core.Entities;
using Arnoosha.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Arnoosha.API.Controllers
{
    public class BasketController : CustomControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _repository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updatedBasket = await _repository.UpdateBasketAsync(basket);

            return updatedBasket;
        }

        [HttpDelete]
        public async Task DeleteBasketById(string id)
        {
            await _repository.DeleteBasketAsync(id);
        }
    }
}
