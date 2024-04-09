using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text;

namespace BlogersProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        public async Task<IActionResult> AddBlog()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBlog(Blog B)
        {

            try
            {
                var jsonData = JsonConvert.SerializeObject(B);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var result = await client.PostAsync("https://localhost:7053/api/Blogs/CreateBlog", content);
                    if (result.IsSuccessStatusCode)
                    {
                        var a = Convert.ToBoolean(await result.Content.ReadAsStringAsync());
                        if (a)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Eror", "Home");
                        }
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Eror", "Home");
            }
            return RedirectToAction("Eror", "Home");
        }
    }
}
