using BlogersProject.Model.Abstract;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IGenericModel<Blog> _db;

        public BlogController(IGenericModel<Blog> db)
        {
            _db = db;
        }

        [HttpGet("GetBlogs")]
        public List<Blog> GetAll()
        {
            return _db.GetList();
        }
        [HttpGet("GetBlog")]
        public Blog GetById(int id)
        {
            return _db.GetRecord(id);
        }
        [HttpPost("AddBlog")]
        public string CreateBlog(Blog B)
        {
            return _db.Insert(B) ? "Bloğunuz Başarılı Bir Şekilde Kaydedilmiştir" : "Bloğunuz Kaydederken Hata Oluştu !";
        }
        [HttpPut("UpdateBlog")]
        public string UpdateBlog(Blog B)
        {
            return _db.Update(B) ? "Bloğunuz Başarılı Bir Şekilde Güncellenmiştir" : "Bloğunuz Güncellenirken Hata Oluştu !";
        }
        [HttpDelete("DeleteBlog")]
        public string RemoveBlog(Blog B)
        {
            return _db.Delete(B) ? "Bloğunuz Başarılı Bir Şekilde Silinmiştir" : "Bloğunuz Silinirken Hata Oluştu !";
        }
    }
}
