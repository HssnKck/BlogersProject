using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnapprovedUsersController : ControllerBase
    {
        private readonly Context _db;
        public UnapprovedUsersController(Context db)
        {
            _db = db;
        }
        [HttpGet("GetList")]
        public List<UnapprovedUser> GetAll()
        {
            return _db.UnapprovedUsers.ToList();
        }
        [HttpGet("GetRecord")]
        public UnapprovedUser GetByID(int id)
        {
            return _db.UnapprovedUsers.Find(id);
        }
        [HttpPost("CreateUnapprovedUser")]
        public bool AddRecord(UnapprovedUser uU)
        {
            try
            {
                _db.UnapprovedUsers.Add(uU);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost("ConfirmBlogger")]
        public bool AddBlogger(UnapprovedUser uU)
        {
            try
            {
                var user = new User();
                user.UserName = uU.UserName;
                user.Name = uU.Name;
                user.Phone = uU.Phone;
                user.Email = uU.Email;
                user.Password = uU.Password;
                user.Role = uU.Role;
                _db.Users.Add(user);
                _db.UnapprovedUsers.Remove(uU);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpDelete("DeleteUnapprovedUser")]
        public bool DeleteRecord(int id)
        {
            try
            {
                var Record = _db.UnapprovedUsers.Find(id);
                _db.UnapprovedUsers.Remove(Record);
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
