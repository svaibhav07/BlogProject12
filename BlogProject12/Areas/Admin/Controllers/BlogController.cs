using BlogProject.Models;
using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
using BlogProject12.Utilities;
//using BlogProject12.Utilities.EmailConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject12.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public BlogController(IUnitOfWork unitOfWork)
        {
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
            UserModel user = new UserModel();
            blogFromDb = _unitOfWork.Blog.Get(id.GetValueOrDefault());
            id = blogFromDb.UserId;
            user = _unitOfWork.User.Get(id.GetValueOrDefault());
            return View(Tuple.Create(blogFromDb, user));

        }

        public IActionResult ApproveBlog(int? id)
        {
            BlogModel blog = new BlogModel();
            UserModel user = new UserModel();
            blog = _unitOfWork.Blog.Get(id.GetValueOrDefault());
            blog.IsApproved = 1;
            id = blog.UserId;
           // var email = blog.User.Email;
            user = _unitOfWork.User.Get(id.GetValueOrDefault());
            _unitOfWork.Blog.Update(blog);
            _unitOfWork.Save();
            string rec = user.Email;
            EmailConfig.SendMail(rec, "BlogWorld-Blog Approved!!", "Your Blog is Approved");
            return Content("Approved");
        }

        public IActionResult RejectBlog(int? id)
        {
            BlogModel blog = new BlogModel();
            UserModel user = new UserModel();
            blog = _unitOfWork.Blog.Get(id.GetValueOrDefault());
            blog.IsRejected = 1;
            id = blog.UserId;
            user = _unitOfWork.User.Get(id.GetValueOrDefault());
            _unitOfWork.Blog.Update(blog);
            _unitOfWork.Save();
            string rec = user.Email;
            EmailConfig.SendMail(rec, "BlogWorld-Blog Rejected !!", "Your Blog is Rejected");

            return Content("Rejected");
        }

        public IActionResult ChangeBlog(int? id)
        {

            BlogModel blog = new BlogModel();
            UserModel user = new UserModel();
            int blogid = (int)id;
            blog = _unitOfWork.Blog.Get(id.GetValueOrDefault());
            blog.ChangeRequested = 1;
            id = blog.UserId;
            user = _unitOfWork.User.Get(id.GetValueOrDefault());
            _unitOfWork.Blog.Update(blog);
            _unitOfWork.Save();
            string rec = user.Email;
            //string rec = blog.User.Email;
           // EmailConfig.SendMail(rec, "BlogWorld- Change Required", "Your Blog require some changes");
            return View(blog);

        }

        [HttpPost]
        public IActionResult ChangeBlog(string change_message,int? id)
        {

            BlogModel blog = new BlogModel();
            UserModel user = new UserModel();
            blog = _unitOfWork.Blog.Get(id.GetValueOrDefault());
            blog.ChangeRequested = 1;
            id = blog.UserId;
            user = _unitOfWork.User.Get(id.GetValueOrDefault());
            _unitOfWork.Blog.Update(blog);
            _unitOfWork.Save();
            //string rec = user.Email;
            string rec = blog.User.Email;
            EmailConfig.SendMail(rec, "BlogWorld- Change Required", "Your Blog require some changes");
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Upsert(int? id)
        {
            BlogModel blog = new BlogModel();
            if (id == null)
            {
                //create
                return View(blog);
            }

            blog = _unitOfWork.Blog.Get(id.GetValueOrDefault());

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                if (blog.Id == 0)
                {
                    _unitOfWork.Blog.Add(blog);

                }
                else
                {
                    _unitOfWork.Blog.Update(blog);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(blog);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Blog.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Blog.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Blog.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });


        }





        #endregion
    }
}
