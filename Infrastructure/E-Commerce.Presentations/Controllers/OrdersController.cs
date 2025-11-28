using E_Commerce.Services.Abstractions.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.OrderDTOs;
using System.Security.Claims;

namespace E_Commerce.Presentations.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManger serviceManger) : ApiControllerBase
    {
        // Create a new order for the authenticated user
        [HttpPost] // POST:BaseUrl/api/Orders
        public async Task<ActionResult<OrderResult>> Create(OrderRequest request)
        {
            // Email of the authenticated user
            var email = User.FindFirstValue(ClaimTypes.Email);

            //Create order command
            var order = await serviceManger.OrderService.CreateOrderAsync(request, email);

            return Ok(order);
        }

        // Get all orders for the authenticated user
        [HttpGet] // GET: BaseUrl/api/Orders
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrders()
        {
            // Email of the authenticated user
            var email = User.FindFirstValue(ClaimTypes.Email);

            // Get orders query
            var orders = await serviceManger.OrderService.GetOrdersByEmailAsync(email);

            return Ok(orders);
        }

        // Get a specific order by ID for the authenticated user
        [HttpGet("{id}")] // GET: BaseUrl/api/Orders/{id}
        public async Task<ActionResult<OrderResult>> GetOrder(Guid id)
        {
            // Get order by ID query
            var order = await serviceManger.OrderService.GetOrderByIdAsync(id);

            return Ok(order);
        }

        // Get Delivery methods available
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")] // GET: BaseUrl/api/Orders/DeliveryMethods
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        {
            // Get delivery methods query
            var methods = await serviceManger.OrderService.GetDeliveryMethodsAsync();

            return Ok(methods);
        }
    }
}
