using BlogProject12.DataAccess.Data;
using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogProject12.DataAccess.Repository
{
    public class BlogRepository : Repository<BlogModel>, IBlogRepository
    {
        private readonly ApplicationDbContext _db;

        public BlogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
        public void Update(BlogModel blog)
        {
            var objFromDb = _db.BlogModel.FirstOrDefault(s => s.Id == blog.Id);
            if (objFromDb != null && blog.IsApproved==1)
            {
                objFromDb.IsApproved = blog.IsApproved;

                _db.SaveChanges();

            }

            if (objFromDb != null && blog.ChangeRequested == 1)
            {
                objFromDb.ChangeRequested = blog.ChangeRequested;

                _db.SaveChanges();

            }

            if (objFromDb != null && blog.IsRejected == 1)
            {
                objFromDb.IsRejected = blog.IsRejected;

                _db.SaveChanges();

            }

            if (blog.User != null && blog.Tag != null)
            {
                _db.SaveChanges();
            }
        }
    }
}
