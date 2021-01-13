using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject12.Areas.User.Controllers
{
    [Area("User")]
    public class UserContoller : Controller
    {

        
        public IActionResult Index()
        {
            return View();
        }
    }
}
