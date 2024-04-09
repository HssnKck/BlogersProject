using BlogersProject.Model.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public IActionResult Eror()
        {
            return View();
        }




    }
}
