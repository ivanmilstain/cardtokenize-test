using System;
using System.Net.Http.Headers;
using CardTokenizationTest.Models;
using CardTokenizationTest.Services.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using static CardTokenizationTest.Models.AppConfigs;

namespace CardTokenizationTest.Services
{
    public class CardServices : ICardServices
    {
        private readonly VaultApi _cardConfig;
        private readonly HttpClient _httpClient;

        public CardServices(HttpClient httpClient, IOptions<VaultApi> options)
        {
            _httpClient = httpClient;
            _cardConfig = options.Value;
        }

        public async Task<Card> GetCardAsync(string cardToken, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var url = new Uri(_cardConfig.BaseUrl + _cardConfig.GetCard + cardToken);
            var response = await _httpClient.GetAsync(url);

            var dataResponse = await response.Content.ReadFromJsonAsync<BaseResult<Card>>();

            return dataResponse.Content;
        }

        public async Task<PaymentPayResponse> Pay(string paymentId, string accessToken, PaymentPayModel request)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var url = new Uri(_cardConfig.BaseUrl + _cardConfig.Pay);
            PaymentPayRequest model = new PaymentPayRequest();
            model.PaymentId = paymentId;
            model.Card.PaymentToken = request.PaymentToken;
            model.Card.PaymentCardType = request.CardType;
            var response = await _httpClient.PostAsJsonAsync(url, model);

            var dataResponse = await response.Content.ReadFromJsonAsync<BaseResult<PaymentPayResponse>>();

            return dataResponse.Content;
        }
    }
}

