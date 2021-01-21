using BlogProject.Models;
using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
using BlogProject12.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject12.Areas.Blog.Controllers
{
    [Area("Blog")]
    //[Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        


      

        public IActionResult Index()
        {
      
            IEnumerable<BlogModel> blogList = _unitOfWork.Blog.GetAll();


            

            return View(blogList);


            
        }

        public IActionResult Create()
        {
            BlogModel blog = new BlogModel();

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogModel blog)
        {
            /*BlogModel blog = new BlogModel();

            blog.BlogTitle = BlogTitle;
            blog.BlogRaw = BlogRaw;
    
            */
            blog.TagId = 3;
            blog.UserId = 1;
            _unitOfWork.Blog.Add(blog);
            _unitOfWork.Save();


            return RedirectToAction(nameof(Index));
        }

       // [Authorize(Roles = "admin")]
        public IActionResult Detail(int? id)
        {
            BlogModel blogFromDb = new BlogModel();
            UserModel user = new UserModel(); 
            blogFromDb = _unitOfWork.Blog.Get(id.GetValueOrDefault());
            int uid = blogFromDb.UserId;
            user =  _unitOfWork.User.Get(id.GetValueOrDefault());
            return View(Tuple.Create(blogFromDb, user));

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
