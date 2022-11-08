using System;
using CardTokenizationTest.Services.Models;

namespace CardTokenizationTest.Models
{
    public class PaymentViewModel
    {
        public List<Card> Cards { get; set; } = new List<Card>();
        public string CardToken { get; set; }
    }
}

