using BlogProject.Models;
using BlogProject12.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject12.DataAccess.Data
{
    
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {

            }

            public DbSet<UserModel> UserModel { get; set; }
            public DbSet<BlogModel> BlogModel { get; set; }
            public DbSet<TagModel> TagModel { get; set; }
    }


    
}
