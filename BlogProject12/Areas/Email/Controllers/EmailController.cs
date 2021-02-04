using BlogProject12.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject12.Areas.Email
{
    [Area("Email")]
    public class EmailController : Controller
    {
        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(string userEmail)
        {

            EmailConfig.SendMail(userEmail, "BlogWorld-Blog Approved!!", "Your Blog is Approved");
            return View();
        }
    }
}
