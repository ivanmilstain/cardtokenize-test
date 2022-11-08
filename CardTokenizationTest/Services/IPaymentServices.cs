using CardTokenizationTest.Services.Models;

namespace CardTokenizationTest.Services
{
    public interface IPaymentServices
    {
        Task<PaymentCreateResponse> Create(string token);
    }
}