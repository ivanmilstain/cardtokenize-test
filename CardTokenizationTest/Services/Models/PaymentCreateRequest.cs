using System;
namespace CardTokenizationTest.Services.Models
{
    public class PaymentCreateRequest
    {
        public string collector_cuit { get; set; }
        public int collector_branchOffice { get; set; }
        public string description { get; set; } = "Pago de prueba";
        public decimal totalAmount { get; set; } = 1800;
        public string currency { get; set; } = "ARS";
        public string paymentService { get; set; } = "MIIII";
        public int channel { get; set; } = 2;
        public List<Item> items { get; set; } = new List<Item>
        {
            new Item()
        };
        public class Item
        {
            public string description { get; set; } = "Item";
            public decimal amount { get; set; } = 1800;
            public int quantity { get; set; } = 1;
        }
    }
}

