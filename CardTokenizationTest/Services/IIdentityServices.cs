using CardTokenizationTest.Services.Models;

namespace CardTokenizationTest.Services
{
    public interface IIdentityServices
    {
        Task<AuthorizationResponse> LoginVault(AuthorizationRequest model);
        Task<AuthorizationResponse> LoginPayment(AuthorizationRequest model);
    }
}