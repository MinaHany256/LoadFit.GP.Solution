using LoadFit.Core;
using LoadFit.Core.Entities;
using LoadFit.Core.Helpers;
using LoadFit.Core.Order_Aggregate;
using LoadFit.Core.Repositories.Contract;
using LoadFit.Core.Services.Contract;
using LoadFit.Core.Specifications.OrderSpecifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly decimal VolumePricePerCubicMeter = 200;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(string PaymentIntentId, string ClientSecret)?> CreateOrUpdatePaymentIntent(decimal subtotal)
        {
            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            // Create new PaymentIntent if needed (assuming we don’t have one yet)
            var options = new PaymentIntentCreateOptions()
            {
                Amount = (long)(subtotal * 100),
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            paymentIntent = await service.CreateAsync(options);

            return (paymentIntent.Id, paymentIntent.ClientSecret);
        }

        //public async Task<Order?> CreateOrUpdatePaymentIntent(string basketId, string buyerEmail)
        //{
        //    // Secret Key
        //    StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];

        //    //Get Basket 
        //    var basket = await _basketRepository.GetBasketAsync(basketId);
        //    if (basket == null) return null;

        //    // Get the associated order using the buyer's email
        //    var orderSpec = new OrderByBuyerEmailSpecification(buyerEmail);
        //    var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(orderSpec);
        //    if (order == null) return null;

        //    // Use the vehicle from the order
        //    var vehicle = order.Vehicle;
        //    if (vehicle == null) return null;

        //    // 4. Calculate total volume in m³
        //    decimal totalVolumeInMeters = basket.Items.Sum(item => item.Volume / 1_000_000m);

        //    // 5. Calculate subtotal using the helper method
        //    decimal subtotal = OrderHelper.CalculateSubtotal(vehicle.price, totalVolumeInMeters, VolumePricePerCubicMeter);

        //    var service = new PaymentIntentService();
        //    PaymentIntent paymentIntent;

        //    if (string.IsNullOrEmpty(order.PaymentIntentId))  // Create
        //    {
        //        var options = new PaymentIntentCreateOptions()
        //        {
        //            Amount = (long)subtotal * 100,
        //            Currency = "usd",
        //            PaymentMethodTypes = new List<string>() { "Card" }
        //        };

        //        paymentIntent = await service.CreateAsync(options);
        //        order.PaymentIntentId = paymentIntent.Id;
        //        order.ClientSecret = paymentIntent.ClientSecret;
        //    }
        //    else  // Update
        //    {
        //        var options = new PaymentIntentUpdateOptions()
        //        {
        //            Amount = (long)subtotal * 100
        //        };
        //        paymentIntent = await service.UpdateAsync(order.PaymentIntentId, options);
        //        order.PaymentIntentId = paymentIntent.Id;
        //        order.ClientSecret = paymentIntent.ClientSecret;
        //    }


        //    _unitOfWork.Repository<Order>().UpdateAsync(order);
        //    await _unitOfWork.CompleteAsync(); // Save changes

        //    return order; 
        //}

    }
}
