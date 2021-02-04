using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProject.Models;
using BlogProject12.Models;



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


            return Content("done");
        }










        public IActionResult RazorpayPayment()
        {

            return View();
        
        }



        [HttpPost]
        public ActionResult CreateOrder(PaymentInitiateModel _requestData)
        {
            // Generate random receipt number for order
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_jgSfZxfkVFDQ0t", "lQz5sINmfOOXXKmM6zPT225T");
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", _requestData.amount * 100);  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "1"); // 1 - automatic  , 0 - manual
                                                 //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            //string orderId = orderResponse["id"].ToString();

            // Create order model for return on view
            OrderModel orderModel = new OrderModel
            {
                //orderId = orderResponse.Attributes["id"],
                razorpayKey = "rzp_test_jgSfZxfkVFDQ0t",
                amount = _requestData.amount * 100,
                currency = "INR",
                name = _requestData.name,
                email = _requestData.email,
                contactNumber = _requestData.contactNumber,
                address = _requestData.address,
                description = "Testing description"
            };

            // Return on PaymentPage with Order data
            return View("PaymentPage", orderModel);
        }

        public class OrderModel
        {
            //public string orderId { get; set; }
            public string razorpayKey { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string contactNumber { get; set; }
            public string address { get; set; }
            public string description { get; set; }
        }


        [HttpPost]
        public ActionResult Complete()
        {
            // Payment data comes in url so we have to get it from url

            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server

            string paymentId = Request.Form["rzp_paymentid"];
            //string paymentId = paymentid;
            // This is orderId
            string orderId = Request.Form["rzp_orderid"];
            

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_jgSfZxfkVFDQ0t", "lQz5sINmfOOXXKmM6zPT225T");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];

            //// Check payment made successfully

            if (paymentCaptured.Attributes["status"] == "captured")
            {
                // Create these action method
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }
    }
}


    

