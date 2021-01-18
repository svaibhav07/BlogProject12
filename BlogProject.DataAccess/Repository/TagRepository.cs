using BlogProject12.DataAccess.Data;
using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogProject12.DataAccess.Repository
{
    public class TagRepository : Repository<TagModel>, ITagRepository
    {
        private readonly ApplicationDbContext _db;

        public TagRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public void Update(TagModel tag)
        {
            var objFromDb = _db.TagModel.FirstOrDefault(s => s.Id == tag.Id);
            if (objFromDb != null)
            {
                objFromDb.TagName = tag.TagName;
                //objFromDb.RelatedBlogs = tag.RelatedBlogs;
                _db.SaveChanges();

            }
        }
    }
}
