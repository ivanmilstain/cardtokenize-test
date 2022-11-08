using System;
using System.Net.Http.Headers;
using CardTokenizationTest.Services.Models;
using Microsoft.Extensions.Options;
using static CardTokenizationTest.Models.AppConfigs;

namespace CardTokenizationTest.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly PaymentApi _paymentConfig;
        private readonly HttpClient _httpClient;

        public PaymentServices(HttpClient httpClient, IOptions<PaymentApi> options)
        {
            _httpClient = httpClient;
            _paymentConfig = options.Value;
        }

        public async Task<PaymentCreateResponse> Create(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = new Uri(_paymentConfig.BaseUrl + _paymentConfig.Create);
            PaymentCreateRequest model = new PaymentCreateRequest();
            model.collector_cuit = _paymentConfig.CollectorCuit;
            model.collector_branchOffice = _paymentConfig.CollectorBranckoffice.Value;
            var response = await _httpClient.PostAsJsonAsync(url, model);

            var dataResponse = await response.Content.ReadFromJsonAsync<BaseResult<PaymentCreateResponse>>();

            return dataResponse.Content;
        }
    }
}

