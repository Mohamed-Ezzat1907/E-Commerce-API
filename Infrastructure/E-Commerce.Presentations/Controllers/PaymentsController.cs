using E_Commerce.Services.Abstractions.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.BasketDTOs;

namespace E_Commerce.Presentations.Controllers
{
    public class PaymentsController(IServiceManger serviceManger) : ApiControllerBase
    {
        [HttpPost("{basketId}")] // POST: BaseURl/api/payments/{basketId}
        public async Task<ActionResult<BasketDTO>> CreateOrUpdatePayment(string basketId)
            => Ok(await serviceManger.PaymentService.CreateOrUpdatePaymentAsync(basketId));

        // Webhook from payment provider
        [HttpPost("WebHook")] // POST: BaseURL/api/Payments/WebHook
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signatureHeader = Request.Headers["Stripe-Signature"];

            await serviceManger.PaymentService.UpdateOrderPaymentAsync(json, signatureHeader!);
            return new EmptyResult();
        }
    }
}
