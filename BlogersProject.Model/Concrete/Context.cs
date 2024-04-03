﻿using BlogersProject.Model.Entities;
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-JERGKNG; Database = BlogsDB; Trusted_Connection = True; TrustServerCertificate = True;");
        }
        public DbSet<Blog> Blogs { get; set; }


    }
}
