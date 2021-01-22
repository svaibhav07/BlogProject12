using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
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


            return View();
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
