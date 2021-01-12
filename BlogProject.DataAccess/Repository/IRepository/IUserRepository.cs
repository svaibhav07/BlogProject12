using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject12.DataAccess.Repository.IRepository
{
   public interface IUserRepository:IRepository<UserModel>
    {
        void Update(UserModel user);
    }
}
