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
        public async Task<IActionResult> Index(string BlogTitle, string BlogFirst, string BlogPost, string Blogger, int UserInt)
        {

            try
            {
                var unapprovedBlog = new UnapprovedBlog();
                unapprovedBlog.BlogTitle = BlogTitle;
                unapprovedBlog.BlogFirst = BlogFirst;
                unapprovedBlog.BlogPost = BlogPost;
                unapprovedBlog.Blogger = Blogger;
                unapprovedBlog.UserInt = UserInt;
                using (var client = new HttpClient())
                {
                
                    var jsondata = JsonConvert.SerializeObject(unapprovedBlog);
                    var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                    var result =  await client.PostAsync("https://localhost:7053/api/UnapprovedBlogs/CreateUnapprovedBlog", content);
                    if (result.IsSuccessStatusCode)
                    {
                        var a = Convert.ToBoolean(await result.Content.ReadAsStringAsync());
                        if (a)
                        {
                            return RedirectToAction("Confirm", "Blogger");
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
        public IActionResult Confirm()
        {
            return View();
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
                        return RedirectToAction("Profile", "Blogger");
                    }
                    else
                    {
                        return RedirectToAction("Eror", "Home");
                    }
                }
                return RedirectToAction("Eror", "Home");
            }
        }
        public async Task<IActionResult> ListComment(int id)
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
                        return RedirectToAction("Profile", "Blogger");
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
