﻿using BlogProject.Models;
using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject12.Areas.Admin.Controllers
{
    [Area("Admin")]
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






        public IActionResult Upsert(int? id)
        {
            UserModel user = new UserModel();
            if (id == null)
            {
                //create
                return View(user);
            }

            user = _unitOfWork.User.Get(id.GetValueOrDefault());
            
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id == 0)
                {
                    _unitOfWork.User.Add(user);
                    
                }
                else 
                {
                    _unitOfWork.User.Update(user);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }







        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.User.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.User.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.User.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        
        
        }



        #endregion
    }
}
