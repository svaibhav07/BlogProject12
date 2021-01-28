using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject12.Areas.Payment.Controllers
{

    [Area("Payment")]
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string stripeToken)
        {
            if (stripeToken == null)
            {

            }
            else 
            {
                var option = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(100000),
                    Currency = "inr",
                    Description = "OrderId",
                    Source = stripeToken

                };

                var service = new ChargeService();
                Charge charge = service.Create(option);

                if (charge.BalanceTransactionId == null)
                {
                    //failed
                }
                else
                {

                    var transid = charge.BalanceTransactionId;
                
                }

                if (charge.Status.ToLower() == "succeeded")
                {
                    var done = charge.Status;
                
                }

            
            }


            return View();
        }
    }
}
