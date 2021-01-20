﻿using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
using BlogProject12.Models.ViewModels;
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
        public IActionResult Detail(int? id)
        {
            BlogModel blogFromDb = new BlogModel();
            blogFromDb = _unitOfWork.Blog.Get(id.GetValueOrDefault());

            return View(blogFromDb);


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
