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
using BlogProject12.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Facebook;
using Newtonsoft.Json.Linq;

namespace BlogProject12.Areas.User.Controllers
{
    [Area("User")]
   // [Authorize]
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
            _unitOfWork.User.Add(user);
            _unitOfWork.Save();

            return RedirectToAction("Index", "Home", new { area = "Blog" });

        }

        public IActionResult UserSignUp()
        {
            UserModel user = new UserModel();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminSignUp(UserModel user)
        {
            
            user.IsAdminRequest = 1;

            _unitOfWork.User.Add(user);
            _unitOfWork.Save();

            return Content("Your request for admin role is sent to superuser. Please wait for the approval ");

        }

        public IActionResult AdminSignUp()
        {
            UserModel user = new UserModel();
            return View(user);
        }


        [AllowAnonymous]
        public IActionResult UserLogin(string returnUrl)
        {

            LoginViewModel model = new LoginViewModel();
            model.ReturnUrl = returnUrl;
          //  model.ExternalLogins = (await signInManager ).ToList();
            
          
            
            return View();
        }

        [Route("facebook-login")]
        [AllowAnonymous]
        public IActionResult FacebookLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("FacebookResponse")

            };

            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        
        }

        [Route("facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            var results = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);


             var claims = results.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
             {

                 claim.Issuer,
                 claim.OriginalIssuer,
                 claim.Type,
                 claim.Value

             });
            //var array = JArray.Parse(claims.FirstOrDefault(s=>s.Value== claims.ToArray));

            var array = claims.Select(s => s.Value);

                    /* array details-:
                     * arr[0]= "Unique ID for User given by facebook" 
                     * arr[1]="Email ID of User"
                     * arr[2]="Full Name"
                     * arr[3]= "First Name"
                     * arr[4]="Last Name"
                     */
            string s="a";
            //return Content(claims.Select(x=>x.Value));
        
            
            foreach (var x in array)
            {
                s= x;
            }
            //return Content(s);
           
             return Json(claims);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserLogin(string UserName, string Password)
        {
            UserModel user = new UserModel();
            //string check = " ";
            user = _unitOfWork.User.GetFirstOrDefault(e => e.UserName == UserName);

            if (Password == user.Password)
            {
                if (user.IsAdminApproved == 1)
                {
                    var identity = new ClaimsIdentity(new[] {
                    new Claim("Id",user.Id.ToString()),                  
                    new Claim("UserName", user.UserName),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Email", user.Email),
                    new Claim("IsAdmin",user.IsAdminApproved.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Blog", new { area = "Admin" });

                }
                else
                {
                    var identity = new ClaimsIdentity(new[] {
                    new Claim("Id",user.Id.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Email", user.Email),
                    new Claim("IsAdmin",user.IsAdminApproved.ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                    return RedirectToAction("Index", "Home", new { area = "Blog" });
                }

            }
            else
            {
                ViewData["Message"] = "Incorrect username or password";
                return View("Index");
            }

            //return View();
            
            }

      


        public async Task<IActionResult> Logout()
            {
            await HttpContext.SignOutAsync();
            return RedirectToAction("UserLogin", "User", new { area = "User" });
        }

           



            public IActionResult SuperUserSection()
            { 
                IEnumerable<UserModel> UserList = _unitOfWork.User.GetAll();
                return View(UserList);
            }

            public IActionResult SuperUserAdminAprove(int? id)
            {
                UserModel user = new UserModel();
                user = _unitOfWork.User.Get(id.GetValueOrDefault());
                user.IsAdminApproved = 1;
                _unitOfWork.User.Update(user);
                return Content("Approved");
            }





    }
    }

