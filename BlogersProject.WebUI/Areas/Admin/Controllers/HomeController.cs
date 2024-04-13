using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogersProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
		public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7053/api/Blogs/GetList");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var blog = JsonConvert.DeserializeObject<List<Blog>>(content);
                    return View(blog);
                }
                else
                {
                    return RedirectToAction("Eror", "Home");
                }
            }
           
        }
        
        public IActionResult Eror()
        {
            return View();
        }




    }
}
