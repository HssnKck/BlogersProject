using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace BlogersProject.WebUI.Controllers
{
    [Authorize(Roles = "Blogger")]
    public class BloggerController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7053/api/Users/GetRecord?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(content);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Eror", "Home");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(Blog B)
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
        public async Task<IActionResult> Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync($"https://localhost:7053/api/Users/GetProfile?username={userName}");

                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<User>(content);
                        return View(user);
                    }
                    else
                    {
                        return RedirectToAction("Eror", "Home");
                    }
                }
            }
            return RedirectToAction("Login", "Index");
        }
        [HttpPost]
        public async Task<IActionResult> Profile(User U)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(U);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var result = await client.PutAsync("https://localhost:7053/api/Users/UpdateUser", content);

                    if (result.IsSuccessStatusCode)
                    {
                        var a = Convert.ToBoolean(await result.Content.ReadAsStringAsync());
                        if (a)
                        {
                            return RedirectToAction("Profile", "Blogger");
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
