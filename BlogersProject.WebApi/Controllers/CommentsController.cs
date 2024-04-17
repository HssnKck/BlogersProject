using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommentersProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly Context _db;

        public CommentsController(Context db)
        {
            _db = db;
        }
        
        [HttpGet("GetList")]
        public List<Comment> GetAll()
        {
            return _db.Comments.ToList();
        }

        [HttpGet("GetListById")]
        public List<Comment> GetAllById(int id)
        {
            return _db.Comments.Where(x=>x.BlogId == id).ToList();
        }

        [HttpGet("GetRecord")]
        public Comment GetByID(int id)
        {
            return _db.Comments.Find(id);
        }
        
        [HttpPost("CreateComment")]
        public bool AddRecord(Comment B)
        {
            try
            {
                _db.Comments.Add(B);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        [HttpPut("UpdateComment")]
        public bool UpdateRecord(Comment B)
        {
            try
            {
                var Record = _db.Comments.Find(B.Id);
                Record.Commentators = B.Commentators;
                Record.Comments = B.Comments;
                Record.CommentDate = DateTime.Now;
                _db.Comments.Update(Record);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        [HttpDelete("DeleteComment")]
        public bool DeleteRecord(int id)
        {
            try
            {
                var Record = _db.Comments.Find(id);
                _db.Comments.Remove(Record);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
