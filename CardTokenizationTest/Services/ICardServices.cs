using CardTokenizationTest.Models;
using CardTokenizationTest.Services.Models;

namespace CardTokenizationTest.Services
{
    public interface ICardServices
    {
        Task<Card> GetCardAsync(string cardToken, string accessToken);
        Task<PaymentPayResponse> Pay(string paymentId, string accessToken, PaymentPayModel request);
    }
}