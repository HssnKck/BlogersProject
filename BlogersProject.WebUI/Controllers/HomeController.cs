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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Privacy(Blog B)
        {
            _db.Blogs.Add(B);

            return  _db.SaveChanges() > 0 ? RedirectToAction("Index") : View();
        }
    }
}
