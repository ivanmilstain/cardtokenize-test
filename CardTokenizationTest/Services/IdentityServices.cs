using System;
using System.IdentityModel.Tokens.Jwt;
using CardTokenizationTest.Services.Models;
using Microsoft.Extensions.Options;
using static CardTokenizationTest.Models.AppConfigs;

namespace CardTokenizationTest.Services
{
    public class IdentityServices : IIdentityServices
    {
        private readonly IdentityApi _identityConfig;
        private readonly HttpClient _httpClientVault;
        private readonly HttpClient _httpClientPayment;

        public IdentityServices(HttpClient httpClientVault, HttpClient httpClientPayment, IOptions<IdentityApi> options)
        {
            _httpClientVault = httpClientVault;
            _httpClientPayment = httpClientPayment;
            _identityConfig = options.Value;
        }

        public async Task<AuthorizationResponse> LoginVault(AuthorizationRequest model)
        {
            _httpClientVault.BaseAddress = new Uri(_identityConfig.BaseUrlVault);

            var response = await _httpClientVault.PostAsJsonAsync(_identityConfig.AuthenticationVault, model);

            var contenido = await response.Content.ReadFromJsonAsync<BaseResult<AuthorizationResponse>>();

            if (contenido.Success)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                contenido.Content.Token = tokenHandler.ReadJwtToken(contenido.Content.AccessToken);
            }

            return contenido.Content;
        }

        public async Task<AuthorizationResponse> LoginPayment(AuthorizationRequest model)
        {
            _httpClientPayment.BaseAddress = new Uri(_identityConfig.BaseUrlPayment);

            var response = await _httpClientPayment.PostAsJsonAsync(_identityConfig.AuthenticationPayment, model);

            var contenido = await response.Content.ReadFromJsonAsync<BaseResult<AuthorizationResponse>>();

            if (contenido.Success)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                contenido.Content.Token = tokenHandler.ReadJwtToken(contenido.Content.AccessToken);
            }

            return contenido.Content;
        }
    }
}

