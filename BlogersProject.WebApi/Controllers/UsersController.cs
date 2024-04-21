using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _db;


        public UsersController(Context db)
        {
            _db = db;
        }
        [HttpGet("GetList")]
        public List<User> GetAll()
        {
            return _db.Users.ToList();
        }
        [HttpGet("GetRecord")]
        public User GetByID(int id)
        {
            return _db.Users.Find(id);
        }
        [HttpGet("GetUser")]
        public User GetByNameAndPassword(string username, string password)
        {
            return _db.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);
        }
        [HttpPost("CreateUser")]
        public bool AddRecord(User B)
        {
            try
            {
                _db.Users.Add(B);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut("UpdateUser")]
        public bool UpdateRecord(User B)
        {
            try
            {
                var Record = _db.Users.Find(B.Id);
                Record.Name = B.Name;
                Record.Phone= B.Phone;
                Record.Email= B.Email;
                Record.Password= B.Password;
                _db.Users.Update(Record);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpDelete("DeleteUser")]
        public bool DeleteRecord(int id)
        {
            try
            {
                var Record = _db.Users.Find(id);
                _db.Users.Remove(Record);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
