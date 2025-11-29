using Shared.Dtos.BasketDTOs;

namespace E_Commerce.Services.Abstractions.Contracts
{
    public interface IPaymentService
    {
        // Payment integration
        public Task<BasketDTO> CreateOrUpdatePaymentAsync(string basketId);
        // Webhook from payment provider
        public Task UpdateOrderPaymentAsync(string json , string signatureHeader);
    }
}
