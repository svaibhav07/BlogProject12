using BlogProject12.DataAccess.Data;
using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
