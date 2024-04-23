using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BlogersProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BloggerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7053/api/UnapprovedUsers/GetList");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var unapprovedUsers = JsonConvert.DeserializeObject<List<UnapprovedUser>>(content);
                    return View(unapprovedUsers);
                }
                else
                {
                    return RedirectToAction("Eror", "Home");
                }
            }
        }
        public async Task<IActionResult> List()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7053/api/Users/GetBloggers");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(content);
                    return View(users);
                }
                else
                {
                    return RedirectToAction("Eror", "Home");
                }
            }
        }
        public async Task<IActionResult> ConfirmUser(int id)
        {
            using (var client = new HttpClient())
            {
                var Uusers = await GetRecord(id);
                var jsonData = JsonConvert.SerializeObject(Uusers);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://localhost:7053/api/UnapprovedUsers/ConfirmBlogger", content);
                if (result.IsSuccessStatusCode)
                {
                    var a = Convert.ToBoolean(await result.Content.ReadAsStringAsync());
                    if (a)
                    {
                        return RedirectToAction("List", "Blogger");
                    }
                    else
                    {
                        return RedirectToAction("Eror", "Home");
                    }
                }
                return RedirectToAction("Eror", "Home");
            }
        }
        public async Task<IActionResult> RemoveUser(int id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"https://localhost:7053/api/UnapprovedUsers/DeleteUnapprovedUser?id={id}");
                if (result.IsSuccessStatusCode)
                {
                    var a = Convert.ToBoolean(await result.Content.ReadAsStringAsync());
                    if (a)
                    {
                        return RedirectToAction("Index", "Blogger");
                    }
                    else
                    {
                        return RedirectToAction("Eror", "Home");
                    }
                }
                return RedirectToAction("Eror", "Home");
            }
        }
        public async Task<IActionResult> DeleteUser(int id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"https://localhost:7053/api/Users/DeleteUser?id={id}");
                if (result.IsSuccessStatusCode)
                {
                    var a = Convert.ToBoolean(await result.Content.ReadAsStringAsync());
                    if (a)
                    {
                        return RedirectToAction("List", "Blogger");
                    }
                    else
                    {
                        return RedirectToAction("Eror", "Home");
                    }
                }
                return RedirectToAction("Eror", "Home");
            }
        }
            private async Task<UnapprovedUser> GetRecord(int id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"https://localhost:7053/api/UnapprovedUsers/GetRecord?id={id}");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UnapprovedUser>(content);
                }
                else
                {
                    return new UnapprovedUser();
                }
            }
        }





    }
}
