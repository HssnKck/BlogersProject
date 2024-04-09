using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogersProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _db;

        public HomeController(Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Blogs.ToList());
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
