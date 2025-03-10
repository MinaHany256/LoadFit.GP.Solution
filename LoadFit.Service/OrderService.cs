using LoadFit.Core;
using LoadFit.Core.Entities;
using LoadFit.Core.Helpers;
using LoadFit.Core.Order_Aggregate;
using LoadFit.Core.Repositories.Contract;
using LoadFit.Core.Services.Contract;
using LoadFit.Core.Specifications.OrderSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Service
{
    public class OrderService : IOrderService
    {

        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly decimal VolumePricePerCubicMeter = 200;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress, int vehicleId)
        {
            // 1. Get the basket from the repository
            var basket = await _basketRepo.GetBasketAsync(basketId);
            

            // 2. Get the selected vehicle
            var vehicle = await _unitOfWork.Repository<Vehicle>().GetAsync(vehicleId);


            // 3. Calculate the total weight of items in the basket
            decimal totalWeight = basket.Items.Sum(item => (item.Volume / 1_000_000m) * (decimal)item.MaterialType);

            // 4. Ensure the vehicle can carry the total weight
            if (totalWeight > vehicle.MaxWeight)
            {
                throw new InvalidOperationException("The selected vehicle cannot carry the total weight of the items.");
            }

            // 4. Calculate total volume in m³
            decimal totalVolumeInMeters = basket.Items.Sum(item => item.Volume / 1_000_000m);

            // 5. Calculate subtotal using the helper method
            decimal subtotal = OrderHelper.CalculateSubtotal(vehicle.price, totalVolumeInMeters, VolumePricePerCubicMeter);

            // 6. Create order items based on the OrderItem entity
            var orderItems = basket.Items.Select(item => new OrderItem(
                productId: item.Id,
                productName: item.Name,
                length: item.Length,
                width: item.Width,
                height: item.Height,
                materialType: item.MaterialType,
                fragilityType: item.FragilityType,
                quantity: item.Quantity
            )).ToList();

            //  Call PaymentService to create a PaymentIntent using the subtotal
            var paymentResult = await _paymentService.CreateOrUpdatePaymentIntent(subtotal);
            string paymentIntentId = paymentResult?.PaymentIntentId ?? string.Empty;
            string clientSecret = paymentResult?.ClientSecret ?? string.Empty;

            // 7. Create the order
            var order = new Order
            (
                buyerEmail: buyerEmail,
                shippingAddress: shippingAddress,
                vehicle: vehicle,
                items: orderItems,
                volumePricePerCubicMeter: VolumePricePerCubicMeter,
                paymentIntentId: paymentIntentId,
                clientSecret: clientSecret
            );

            

            // 8. Save the order
            await _unitOfWork.Repository<Order>().AddAsync(order);
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return order;

        }

        private decimal GetMaterialDensity(TypeOfMaterial materialType)
        {
            return (decimal)materialType; 
        }


        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecifications(orderId, buyerEmail);
            var order = orderRepo.GetWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await orderRepo.GetAllWithSpecAsync(spec);
            return orders;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForDriverAsync(int driverId)
        {
            var spec = new OrdersForDriverSpecification(driverId);
            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
        }
    }
}
