using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace BlogersProject.WebUI.Controllers
{
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
        public IActionResult Developer()
        {
            return View();
        }
        public IActionResult Eror()
        {
            return View();
        }
    }
}
