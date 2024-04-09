using BlogersProject.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogersProject.Model.Concrete
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContext) : base(dbContext)
        {
            
        }
        public DbSet<Blog> Blogs { get; set; }


    }
}
