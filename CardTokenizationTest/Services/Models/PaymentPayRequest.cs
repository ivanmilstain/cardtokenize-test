using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace CardTokenizationTest.Services.Models
{
    public class PaymentPayRequest
    {
        public string PaymentId { get; set; }
        public decimal CardAmount { get; set; } = 1800;
        public string PaymentMethodDescription { get; set; } = "VISA XXXX-0001";
        public int InstallmentId { get; set; } = 1;
        public int InstallmentQuantity { get; set; } = 1;
        public decimal InstallmentAmount { get; set; } = 1800;
        public PaymentPayCard Card { get; set; } = new PaymentPayCard();
        public PaymentPayUser User { get; set; } = new PaymentPayUser();
        public string Channel { get; set; } = "API";

        public class PaymentPayCard
        {
            public string PaymentType { get; set; } = "CARDS";
            public string PaymentToken { get; set; }
            public string PaymentCardType { get; set; } = "CREDIT";
        }
        public class PaymentPayUser
        {
            public string FirstName { get; set; } = "John";
            public string LastName { get; set; } = "Doe";
            public string DocumentNumber { get; set; } = "33654852";
            public string Email { get; set; } = "johndoe@testvault.com";
        }
    }
}

