using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.RateLimiting;

namespace BlogersProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly Context _db;


        public BlogsController(Context db)
        {
            _db = db;
        }
        [HttpGet("GetList")]
        public List<Blog> GetAll()
        {
            return _db.Blogs.ToList();
        }
        [HttpGet("GetByUser")]
        public List<Blog> GetUserList(int id)
        {
            return _db.Blogs.Where(x=> x.UserId == id).ToList();
        }
        [HttpGet("GetRecord")]
        public Blog GetByID(int id)
        {
            return _db.Blogs.Find(id);
        }
        [HttpPost("CreateBlog")]
        public bool AddRecord(Blog B)
        {
            try
            {
                _db.Blogs.Add(B);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut("UpdateBlog")]
        public bool UpdateRecord(Blog B)
        {
            try
            {
                var Record = _db.Blogs.Find(B.Id);
                Record.BlogTitle = B.BlogTitle;
                Record.BlogFirst= B.BlogFirst;
                Record.BlogPost = B.BlogPost;
                Record.Blogger = B.Blogger;
                Record.UserId = B.UserId;
                Record.BlogDate = DateTime.Now;
                _db.Blogs.Update(Record);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpDelete("DeleteBlog")]
        public bool DeleteRecord(int id)
        {
            try
            {

                foreach (var item in _db.Comments.Where(x => x.BlogId == id).ToList())
                {
                    _db.Comments.Remove(item);
                }
                var Record = _db.Blogs.Find(id);
                _db.Blogs.Remove(Record);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
