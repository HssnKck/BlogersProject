using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnapprovedBlogsController : ControllerBase
    {
        private readonly Context _db;


        public UnapprovedBlogsController(Context db)
        {
            _db = db;
        }
        [HttpGet("GetList")]
        public List<UnapprovedBlog> GetAll()
        {
            return _db.UnapprovedBlogs.ToList();
        }
        [HttpGet("GetRecord")]
        public UnapprovedBlog GetByID(int id)
        {
            return _db.UnapprovedBlogs.Find(id);
        }
        [HttpPost("CreateUnapprovedBlog")]
        public bool AddRecord(UnapprovedBlog Ub)
        {
            try
            {
                _db.UnapprovedBlogs.Add(Ub);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost("ConfirmBlog")]
        public bool AddBlog(UnapprovedBlog uB)
        {
            try
            {
                var blog = new Blog();
                blog.BlogTitle = uB.BlogTitle;
                blog.BlogFirst = uB.BlogFirst;
                blog.BlogPost = uB.BlogPost;
                blog.Blogger = uB.Blogger;
                blog.BlogDate = uB.BlogDate;
                blog.UserId = uB.UserInt;
                _db.Blogs.Add(blog);
                _db.UnapprovedBlogs.Remove(uB);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpDelete("DeleteUnapprovedBlog")]
        public bool DeleteRecord(int id)
        {
            try
            {
                var Record = _db.UnapprovedBlogs.Find(id);
                _db.UnapprovedBlogs.Remove(Record);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

