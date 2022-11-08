using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace CardTokenizationTest.Services.Models
{
    public class AuthorizationResponse
    {
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }

        [JsonPropertyName("accessToken")]
        public string? AccessToken { get; set; }

        public JwtSecurityToken? Token { get; set; }
    }
}

