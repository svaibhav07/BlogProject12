using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject12.Areas.GoogleMap.Controllers
{
    [Area("GoogleMap")]
    public class MapController : Controller
    {
        public IActionResult MyMap1()
        {
            return View();
        }

        public IActionResult MyMap2()
        {


            return View();
        }
    }
}
