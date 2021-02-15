using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject12.Areas.GoogleMap.Controllers
{
    [Area("GoogleMaps")]
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
