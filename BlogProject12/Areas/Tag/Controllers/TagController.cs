using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject12.Areas.Tag.Controllers
{
    [Area("Tag")]
    public class TagController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
