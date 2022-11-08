using System;
namespace CardTokenizationTest.Models
{
    public class AppConfigs
    {
        public class IdentityApi
        {
            public string? BaseUrlVault { get; set; }
            public string? AuthenticationVault { get; set; }
            public string? UsernameVault { get; set; }
            public string? PasswordVault { get; set; }
            public string? BaseUrlPayment { get; set; }
            public string? AuthenticationPayment { get; set; }
            public string? UsernamePayment { get; set; }
            public string? PasswordPayment { get; set; }
            public string? IframeSdk { get; set; }
        }

        public class VaultApi
        {
            public string? BaseUrl { get; set; }
            public string? GetCard { get; set; }
            public string? Pay { get; set; }
        }

        public class PaymentApi
        {
            public string? BaseUrl { get; set; }
            public string? Create { get; set; }
            public string? CollectorCuit { get; set; }
            public int? CollectorBranckoffice { get; set; }
        }
    }
}

