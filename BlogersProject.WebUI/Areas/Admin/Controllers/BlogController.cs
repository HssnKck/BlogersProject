using Azure;
using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text;

namespace BlogersProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        public IActionResult AddBlog()
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
        public async Task<IActionResult> UpdateBlog(int id)
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
        public async Task<IActionResult> UpdateBlog(Blog B)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(B);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var result = await client.PutAsync("https://localhost:7053/api/Blogs/UpdateBlog", content);

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
        public async Task<IActionResult> DeleteBlog(int id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"https://localhost:7053/api/Blogs/DeleteBlog?id={id}");
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
                return RedirectToAction("Eror", "Home");
            }
        }
    }
}