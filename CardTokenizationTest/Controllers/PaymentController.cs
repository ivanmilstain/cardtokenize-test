using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardTokenizationTest.Database;
using CardTokenizationTest.Models;
using CardTokenizationTest.Services;
using CardTokenizationTest.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static CardTokenizationTest.Models.AppConfigs;

namespace CardTokenizationTest.Controllers
{
    public class PaymentController : Controller
    {
        private readonly TokenizationContext _context;
        private readonly ICardServices _cardServices;
        private readonly IIdentityServices _identityServices;
        private readonly IdentityApi _identityConfig;
        private readonly IPaymentServices _paymentServices;

        public PaymentController(TokenizationContext context, ICardServices cardServices, IIdentityServices identityServices, IOptions<IdentityApi> optionsIdentity, IPaymentServices paymentServices)
        {
            _context = context;
            _cardServices = cardServices;
            _identityServices = identityServices;
            _identityConfig = optionsIdentity.Value;
            _paymentServices = paymentServices;
        }

        // GET: Payment/Pay/5
        public async Task<IActionResult> Pay(string id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            PaymentViewModel viewModel = new PaymentViewModel();
            var userCards = _context.Cards.Where(x => x.UserId == id).ToList();
            if (userCards == null)
            {
                return NotFound();
            }

            var identity = await _identityServices.LoginVault(new AuthorizationRequest
            {
                UserName = _identityConfig.UsernameVault,
                Password = _identityConfig.PasswordVault
            });

            foreach (var card in userCards)
                viewModel.Cards.Add(await _cardServices.GetCardAsync(card.CardToken, identity.AccessToken));

            ViewData["IframeSdk"] = _identityConfig.IframeSdk;
            ViewData["UsernameVault"] = _identityConfig.UsernameVault;
            ViewData["PasswordVault"] = _identityConfig.PasswordVault;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(PaymentPayModel model)
        {
            var identityPayment = await _identityServices.LoginPayment(new AuthorizationRequest
            {
                UserName = _identityConfig.UsernamePayment,
                Password = _identityConfig.PasswordPayment
            });
            var payment = await _paymentServices.Create(identityPayment.AccessToken);


            var identityVault = await _identityServices.LoginVault(new AuthorizationRequest
            {
                UserName = _identityConfig.UsernameVault,
                Password = _identityConfig.PasswordVault
            });
            await _cardServices.Pay(payment.PaymentId, identityVault.AccessToken, model);

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}