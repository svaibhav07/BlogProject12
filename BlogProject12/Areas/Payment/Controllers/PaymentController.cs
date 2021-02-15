using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProject.Models;
using BlogProject12.Models;
using Microsoft.Extensions.Logging;
using BlogProject12.DataAccess.Repository.IRepository;
//using PaytmChecksum;
using paytm;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using BlogProject12.Models.ViewModels;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using BlogProject12.Utilities.CashfreePayment;
using BlogProject12.Utilities;
using Microsoft.Extensions.Options;
//using Microsoft.Azure.Storage.Shared.Protocol;

namespace BlogProject12.Areas.Payment.Controllers
{

    [Area("Payment")]
    public class PaymentController : Controller
    {

       // private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CashFreeKeys _keys;

        public PaymentController(IUnitOfWork unitOfWork, IOptions<CashFreeKeys> keys)
        {          
            _unitOfWork = unitOfWork;
            _keys = keys.Value;
        }

        //All  the payment starts from index function here

        public IActionResult Index()
        {
            return View();
        }

      

        #region Stripe Payment



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

        #endregion



        #region RazorPay

        /// <summary>
        /// Razorpay
        /// </summary>
        /// <returns></returns>




        public IActionResult RazorpayPayment()
        {
            PaymentInitiateModel payment = new PaymentInitiateModel();
            UserModel user = new UserModel();
            user = _unitOfWork.User.GetFirstOrDefault(e => e.UserName == User.FindFirst("UserName").Value);
            payment.name = user.FirstName;
            payment.email = user.Email;
            payment.contactNumber = "9931159589";
            payment.address = "Ranchi";
            payment.amount = 1;
            payment.UserId = user.Id;
            payment.User = user;
            _unitOfWork.Payment.Add(payment);
            _unitOfWork.Save();
            return View(payment);
        
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



        #endregion

        #region GooglePay

        public IActionResult GooglePayPayment()
        {


            return View();
        }



        #endregion


        #region PaytmPayment

        public IActionResult PaytmPayment()
        {



            return View();
        }


        [HttpPost]
        public ContentResult PaytmPayment(string Order_Id,  string User_Id, string Name, string Email, string Contact_No, string Amount)
        {
            String merchantKey = "M4bjjBIoF96_Jvzw";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", "nZOopB58096118599177");
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", Email);
            parameters.Add("MOBILE_NO", Contact_No);
            parameters.Add("CUST_ID", User_Id);
            parameters.Add("ORDER_ID", Order_Id);
            parameters.Add("TXN_AMOUNT", Amount);
            parameters.Add("CALLBACK_URL", "https://localhost:44381/Payment/Payment/PaytmPaymentCallBack"); //This parameter is not mandatory. Use this to pass the callback url dynamically.
            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
            string paytmURL = "https://securegw-stage.paytm.in/order/process?orderid=" + Order_Id;
            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center>Please do not refresh this page...</center>"; //you can put h1 tag here
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            return base.Content(outputHTML,"text/html");

        }


       [HttpPost]
        public IActionResult PaytmPaymentCallBack(object sender, EventArgs e)
        {
            String merchantKey = "M4bjjBIoF96_Jvzw";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string paytmChecksum = "";
            foreach (string key in Request.Form.Keys)
            {
                string key_trim = Request.Form[key];
                parameters.Add(key.Trim(), key_trim.Trim());
               // parameters.Add(key.Trim(), Request.Form[key].Trim());
            }
            if (parameters.ContainsKey("CHECKSUMHASH"))
            {
                paytmChecksum = parameters["CHECKSUMHASH"];
                parameters.Remove("CHECKSUMHASH");
            }
            if (CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum))
            {
                string paytmStatus = parameters["STATUS"];
                string txnId = parameters["TXNID"];
                string traxid = "Transaction Id : " + txnId;
                if (paytmStatus == "TXN_SUCCESS")
                {
                    return Content("Payment Successful!!");
                }
                else if (paytmStatus == "PENDING")
                {
                    return Content("Payment Pending!!");
                }
                else if (paytmStatus == "TXN_FAILURE")
                {
                    return Content("Payment Failed!!");
                }
                return Content("Checksum Matched");
            }
            else
            {
                return Content("Checksum MisMatch");
            }
        }



        #endregion

        #region CashFree

     /*   public IActionResult Cashfree()
        {


            return View();
        }*/

        public IActionResult Cashfree()
        {
            Payments1 payment = new Payments1();
            UserModel user = new UserModel();
            Random ran = new Random();
            
            user = _unitOfWork.User.GetFirstOrDefault(e => e.UserName == User.FindFirst("UserName").Value);
            payment.UserId = user.Id;
            payment.User = user;
            payment.User.Phone = "7631167103"; //Because Phone Number is not present in Table
            payment.OrderId = ran.Next(1000000000).ToString();//random generated
            //payment.OrderId = payment.PaymentId;
            payment.Ammount = 100;//set amount
            payment.Status = SD.PaymentInitiated;
            ViewData["CallBackUrl"] =SD.PaymentCallBackUrl;
            _unitOfWork.Payments.Add(payment);
            _unitOfWork.Save();
            return View(payment);
        }

        [HttpPost]
        public IActionResult HandleRequest(PaymentForm model)
        {

            string secretKey = _keys.CashFreeSecretKey;
            string mode = "TEST";  //change mode to PROD for production
            string signatureData = "";
            PropertyInfo[] keys = model.GetType().GetProperties();
            keys = keys.OrderBy(key => key.Name).ToArray();

            foreach (PropertyInfo key in keys)
            {
                signatureData += key.Name + key.GetValue(model);
            }
            var hmacsha256 = new HMACSHA256(StringEncode(secretKey));
            byte[] gensignature = hmacsha256.ComputeHash(StringEncode(signatureData));
            string signature = Convert.ToBase64String(gensignature);
            ViewData["signature"] = signature;
            if (mode == "PROD")
            {
                ViewData["url"] = "https://www.cashfree.com/checkout/post/submit";
            }
            else
            {
                ViewData["url"] = "https://test.cashfree.com/billpay/checkout/post/submit";
            }

            Payments1 payment = new Payments1();
            payment = _unitOfWork.Payments.GetFirstOrDefault(e => e.OrderId == model.orderId);
            payment.Status = SD.PaymentPending;
            _unitOfWork.Payments.Update(payment);
            _unitOfWork.Save();
            return View(model);
        }

        [HttpPost]
        public IActionResult HandleResponse(IFormCollection form)
        {
            string secretKey = _keys.CashFreeSecretKey;
            string orderId = Request.Form["orderId"];
            string orderAmount = Request.Form["orderAmount"];
            string referenceId = Request.Form["referenceId"];
            string txStatus = Request.Form["txStatus"];
            string paymentMode = Request.Form["paymentMode"];
            string txMsg = Request.Form["txMsg"];
            string txTime = Request.Form["txTime"];
            string signature = Request.Form["signature"];

            string signatureData = orderId + orderAmount + referenceId + txStatus + paymentMode + txMsg + txTime;

            var hmacsha256 = new HMACSHA256(StringEncode(secretKey));
            byte[] gensignature = hmacsha256.ComputeHash(StringEncode(signatureData));
            string computedsignature = Convert.ToBase64String(gensignature);
            if (signature == computedsignature)
            {
                ViewData["panel"] = "panel panel-success";
                ViewData["heading"] = "Signature Verification Successful";

            }
            else
            {
                ViewData["panel"] = "panel panel-danger";
                ViewData["heading"] = "Signature Verification Failed";

            }
            ViewData["orderId"] = orderId;
            ViewData["orderAmount"] = orderAmount;
            ViewData["referenceId"] = referenceId;
            ViewData["txStatus"] = txStatus;
            ViewData["txMsg"] = txMsg;
            ViewData["txTime"] = txTime;
            ViewData["paymentMode"] = paymentMode;
            if (txStatus == "SUCCESS")
            {
                Payments1 payment = new Payments1();
                payment = _unitOfWork.Payments.GetFirstOrDefault(e => e.OrderId == orderId);
                payment.Status = SD.PaymentSuccessful;
                _unitOfWork.Payments.Update(payment);
                _unitOfWork.Save();
            }
            else
            {
                Payments1 payment = new Payments1();
                payment = _unitOfWork.Payments.GetFirstOrDefault(e => e.OrderId == orderId);
                payment.Status = SD.PaymentFailed;
                _unitOfWork.Payments.Update(payment);
                _unitOfWork.Save();


            }

            return View();
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new UTF8Encoding();
            return encoding.GetBytes(text);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        #endregion







    }
}





