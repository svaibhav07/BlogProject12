using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProject12.Models;
using BlogProject.Models;
using BlogProject12.DataAccess.Repository.IRepository;

namespace BlogProject12.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;


        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserSignUp(UserModel user)
        {
            //  UserModel user = new UserModel();
            //if (ModelState.IsValid)
            //{

                _unitOfWork.User.Add(user);
                _unitOfWork.Save();

                return RedirectToAction("Index", "Home", new { area = "Blog" });
                
            //}

            return View(user);

        }

        public IActionResult UserSignUp( )
        {
            UserModel user = new UserModel();
           

           // user = _unitOfWork.Tag.Get(id.GetValueOrDefault());

           

            return View(user);
        }



        public IActionResult UserLogin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserLogin(string UserName, string Password)
        {
            //UserName = "admin1";
           UserModel user = new UserModel();
            string check = " ";
           user = _unitOfWork.User.GetFirstOrDefault(e => e.UserName == UserName);

            if (Password == user.Password)
            {
                check = "password Matched";
                return Content("Password Matched");
            }
         
           return RedirectToAction("Index", "Home", new { area = "Blog" });

           //return View();
        }

        public IActionResult UserLogout()
        {
            return View();
        }

 

    }
}
