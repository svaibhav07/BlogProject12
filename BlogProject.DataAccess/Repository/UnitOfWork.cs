using BlogProject12.DataAccess.Data;
using BlogProject12.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject12.DataAccess.Repository
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {

            _db = db;
            User = new UserRepository(_db);
            Tag = new TagRepository(_db);
            Blog = new BlogRepository(_db);
            SP_Call = new SP_Call(_db);
        }
        public IUserRepository User
        { get; private set; }

        public ITagRepository Tag
        { get; private set; }

        public IBlogRepository Blog
        { get; private set; }

        public ISP_Call SP_Call 
        { get; private set; }

       

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
