using LoadFit.Core.Order_Aggregate;

namespace LoadFit.Core.Services.Contract
{
    public interface IPaymentService
    {

        // Function for create or update payment intent
        Task<(string PaymentIntentId, string ClientSecret)?> CreateOrUpdatePaymentIntent(decimal subtotal);
    }
}
