using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.OrderAggregate;
using E_Commerce.Domain.Exceptions;
using E_Commerce.Services.Abstractions.Contracts;
using E_Commerce.Services.Specifications;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.BasketDTOs;
using Stripe;
using Product = E_Commerce.Domain.Entities.ProductModule.Product;

namespace E_Commerce.Services.Immplementations
{
    // Payment Intgration
    // 0- Install Stripe Package
    // 1- Set Up API Key [Secret Key]
    // 2- Get Basket By Id From BasketRepo
    // 3- Validate On BasketItems.Price == Product.Price To Get Real Price From DB
    // 4- Get Delivery Method And Shipping Price
    // 5- Retrive Delivery Method From DB And Assign Price Of Basket [Shipping Price] = DeliveryMethod.Price
    // 6- Total Amount = SubTotal + ShippingPrice[(Items.Price * Item.Quantity) + basket.ShippingPrice] * 100 [In Cents]
    // 7- Create Or Update PaymentIntent [If No PaymentIntentId Create New One, If Exists Update It]
    // 8- Save PaymentIntentId And ClientSecret To Basket And Update Basket In Repo
    // 10- Return The Updated BasketDTO
    internal class PaymentService(
        IConfiguration configuration,
        IBasketRepository basketRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IPaymentService
    {
        // Payment integration
        public async Task<BasketDTO> CreateOrUpdatePaymentAsync(string basketId)
        {
            // configure Stripe API Key Using The Secret Key from appsettings.json
            StripeConfiguration.ApiKey = configuration.GetSection("StripeSetting")["SecretKey"];

            // Get Basket By Id From BasketRepo
            var basket = await basketRepository.GetBasketAsync(basketId) ??
                throw new BasketNotFoundException(basketId);

            // Validate On BasketItems.Price == Product.Price To Get Real Price From DB
            foreach (var item in basket.Items) 
            {
                var product = await unitOfWork.GetReository<Product, int>()
                                              .GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);

                // Update The Item Price in Basket With The Real Price From DB
                item.Price = product.Price;
            }

            // Get Delivery Method And Shipping Price if Exists
            if (!basket.DeliveryMethodId.HasValue)
                throw new Exception("Delivery Method is not selected");

            // Retrive Delivery Method From DB And Assign Price Of Basket [Shipping Price] = DeliveryMethod.Price
            var deliveryMethod = await unitOfWork.GetReository<DeliveryMethod, int>()
                                                 .GetByIdAsync(basket.DeliveryMethodId.Value) ?? 
                                                 throw new DeliveryMethodException(basket.DeliveryMethodId.Value);

            basket.ShippingPrice = deliveryMethod.Price;

            // Total Amount = SubTotal + ShippingPrice[(Items.Price * Item.Quantity) + basket.ShippingPrice] * 100 [In Cents]
            var amount = (long)(basket.Items.Sum(i => (i.Price * i.Quantity)) + basket.ShippingPrice) * 100;

            // Create Or Update PaymentIntent [If No PaymentIntentId Create New One, If Exists Update It]
            var StripeService = new PaymentIntentService();

            // If You Need To Create Or Update PaymentIntent
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = [ "card" ]
                };
                var paymentIntent = await StripeService.CreateAsync(createOptions);
            }
            else
            { 
                // Update The PaymentIntent
                // Update The Amount Only [In Case Of Changing The Basket Items Or Delivery Method And The Total Amount Changed]
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await StripeService.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }

            // Save PaymentIntentId And ClientSecret To Basket And Update Basket In Repo
            await basketRepository.CreateOrUpdateBasketAsync(basket);

            // Return The Updated BasketDTO
            return mapper.Map<BasketDTO>(basket);

        }

        // Webhook from payment provider
        public async Task UpdateOrderPaymentAsync(string json, string signatureHeader)
        {
            var endpointSecret = configuration.GetSection("StripeSetting")["EndPointSecret"];
            
                var stripeEvent = EventUtility.ParseEvent(json, throwOnApiVersionMismatch: false);

                stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, endpointSecret, throwOnApiVersionMismatch: false);

                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentIntentSucceeded(paymentIntent!.Id);
                    break;
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentIntentFailed(paymentIntent!.Id);
                     break;
                default:
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
        }

        #region Helper Methods

        private async Task UpdatePaymentIntentSucceeded(string paymentIntentId)
        {
            var orderRepo = unitOfWork.GetReository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId)) 
                                        ?? throw new Exception("No Order With This PaymentIntent");

            order.PaymentStatus = OrderPaymentStatus.PaymentReceived;
            orderRepo.Update(order);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentIntentFailed(string paymentIntentId)
        {
            var orderRepo = unitOfWork.GetReository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId))
                                          ?? throw new Exception("No Order With This PaymentIntent");

            order.PaymentStatus = OrderPaymentStatus.PaymentFailed;
            orderRepo.Update(order);
            await unitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}
