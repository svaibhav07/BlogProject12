using BlogProject.Models;
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
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            
            return View();
        }
    
   
        public IActionResult Upsert(int? id)
        {
            TagModel tag = new TagModel();
            if (id == null)
            {
                //create
                return View(tag);
            }

            tag = _unitOfWork.Tag.Get(id.GetValueOrDefault());
            
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Upsert(TagModel tag)
        {
            if (ModelState.IsValid)
            {
                if (tag.Id == 0)
                {
                    _unitOfWork.Tag.Add(tag);
                    
                }
                else 
                {
                    _unitOfWork.Tag.Update(tag);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }

        public IActionResult Delete(int? id)
        {
            TagModel tag = new TagModel();
            if (id == null)
            {
                //create
                return View(tag);
            }

            tag = _unitOfWork.Tag.Get(id.GetValueOrDefault());

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(TagModel tag)
        {
            var objFromDb = _unitOfWork.Tag.Get(tag.Id);
            string deleteMSG;
            if (objFromDb == null)
            {
                deleteMSG = "Error while deleting";
            }
            else
            {
                _unitOfWork.Tag.Remove(objFromDb);
                _unitOfWork.Save();
                deleteMSG= "Delete Successful";
            }

            return View(deleteMSG);
        }

        #region API CALLS
        [HttpPost]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Tag.GetAll();
            return Json(new { data = allObj });
        }

        [HttpGet]
        public IActionResult DeleteAPI(int id)
        {
            var objFromDb = _unitOfWork.Tag.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Tag.Remove(objFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));


        }

        #endregion
    }
}
