using BlogProject12.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject12.DataAccess.Repository.IRepository
{
   public interface ITagRepository:IRepository<TagModel>
    {

        void Update(TagModel tag);
    }
}
