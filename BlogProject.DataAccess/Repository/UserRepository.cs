using BlogProject.Models;
using BlogProject12.DataAccess.Data;
using BlogProject12.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogProject12.DataAccess.Repository
{
    public class UserRepository:Repository<UserModel>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public void Update(UserModel user)
        {
            var objFromDb = _db.UserModel.FirstOrDefault(s => s.Id == user.Id);
            if(objFromDb!=null)
            {
                objFromDb.UserName = user.FirstName;

                _db.SaveChanges();
            
            }
                }
    }
}
