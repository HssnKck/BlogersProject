using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BlogersProject.WebUI.Controllers
{
    public class ContentController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"https://localhost:7053/api/Blogs/GetRecord?id={id}");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var blog = JsonConvert.DeserializeObject<Blog>(content);
                    return View(blog);
                }
                else
                {
                    return RedirectToAction("Eror", "Home");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(string Name, string Comment,int id)
        {
            try
            {
                var C = new Comment();
                C.Commentators = Name;
                C.Comments = Comment;
                C.BlogId = id;
                var jsonData = JsonConvert.SerializeObject(C);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var result = await client.PostAsync("https://localhost:7053/api/Comments/CreateComment", content);
                    if (result.IsSuccessStatusCode)
                    {
                        var a = Convert.ToBoolean(await result.Content.ReadAsStringAsync());
                        if (a)
                        {
                            return RedirectToAction("Index", "Content",id);
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
