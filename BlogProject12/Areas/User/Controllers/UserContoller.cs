using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProject12.Models;
using BlogProject.Models;
using BlogProject12.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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

        public IActionResult UserSignUp()
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
            UserModel user = new UserModel();
            string check = " ";
            user = _unitOfWork.User.GetFirstOrDefault(e => e.UserName == UserName);
            //HttpContext.Session.SetString("product", "laptop");

            if (Password == user.Password)
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim("Id",user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("UserName", user.UserName),
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                return RedirectToAction("Index", "Home", new { area = "Blog" });

            }
            else
            {
                ViewData["Message"] = "Incorrect username or password";
                return View("Index");
            }

            //return View();



            /*Admin admin = await _unitWork.Admin.FindOneAsync(p => p.UserName == userName &&
            p.Password == password);

            if (admin != null)
            {
                var identity = new ClaimsIdentity(new[] {
                     new Claim(UserClaimType.Id, admin.Id.ToString()),
                     new Claim(UserClaimType.Email, admin.Email),
                     new Claim(ClaimTypes.Role, RoleConst.Admin),
                     new Claim(UserClaimType.FullName, admin.AdminName),
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home", new { area = "Administration" });
            }
            else
            {
                ViewData["Message"] = "Incorrect username or password";
                return View("Index");
            }*/
            
            }



            public IActionResult UserLogout()
            {
                return View();
            }



        }
    }

