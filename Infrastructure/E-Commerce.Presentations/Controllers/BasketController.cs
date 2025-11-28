using E_Commerce.Services.Abstractions.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.BasketDTOs;
using Shared.Dtos.ProductDTOs;

namespace E_Commerce.Presentations.Controllers
{
    [Authorize]
    public class BasketController(IServiceManger serviceManger) : ApiControllerBase
    {
        [ProducesResponseType(typeof(BasketDTO), StatusCodes.Status200OK)]
        [HttpGet("{id}")] // GET: BaseUrl/api/Basket/{id} ==> Basket02
        public async Task<ActionResult<BasketDTO>> Get(string id)
            => Ok(await serviceManger.BasketService.GetBasketAsync(id));

        [HttpPost] // POST: BaseUrl/api/Basket
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO basketDTO)

            => Ok(await serviceManger.BasketService.CreateOrUpdateBasketAsync(basketDTO));

        [HttpDelete("{id}")] // DELETE: BaseUrl/api/Basket/{id} ==> Basket02
        public async Task<ActionResult> Delete(string id)
        {
            await serviceManger.BasketService.DeleteBasketAsync(id);
            return NoContent(); // 204
        }
    }
}
