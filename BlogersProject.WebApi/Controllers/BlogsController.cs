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
                _db.Blogs.Update(B);
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
