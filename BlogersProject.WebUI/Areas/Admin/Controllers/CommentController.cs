using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogersProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"https://localhost:7053/api/Comments/GetListById?id={id}");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var comment = JsonConvert.DeserializeObject<List<Comment>>(content);
                    return View(comment);
                }
                else
                {
                    return RedirectToAction("Eror", "Home");
                }
            }
        }
        public async Task<IActionResult> DeleteComment(int id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"https://localhost:7053/api/Comments/DeleteComment?id={id}");
                if (result.IsSuccessStatusCode)
                {
                    var a = Convert.ToBoolean(await result.Content.ReadAsStringAsync());
                    if (a)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Eror", "Home");
                }
            }
            return RedirectToAction("Eror", "Home");
        }
    }
}

