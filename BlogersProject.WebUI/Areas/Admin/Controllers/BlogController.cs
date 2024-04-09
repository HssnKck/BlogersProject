using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebUI.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        [Area("Admin")]
        public IActionResult GetAll()
        {
            // var result = await client.PostAsync("https://localhost:7267/api/Blog/GetBlogs, content);
            return View();
        }
    }
}
