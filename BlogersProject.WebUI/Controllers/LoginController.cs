using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BlogersProject.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User U)
        {
            var users = await GetListAsync();
            var user = await GetRecordAsync(U.UserName, U.Password);
            foreach (var item in users)
            {
                if (item.UserName == U.UserName && item.Password == U.Password)
                {
                    var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.Email, user.Email),
                                new Claim(ClaimTypes.MobilePhone, user.Phone),
                                new Claim(ClaimTypes.Role, user.Role)
                            };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var userPrincipal = new ClaimsPrincipal(claimsIdentity);
                    var authProperties = new AuthenticationProperties();
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);
                    bool isAdmin = false;

                    foreach (var claim in claims)
                    {
                        if (claim.Type == ClaimTypes.Role && claim.Value == "Admin")
                        {
                            isAdmin = true;
                            break;
                        }
                    }
                    if (isAdmin)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Blogger", new { id = user.Id });
                }
            }
            return RedirectToAction("Eror", "Home");
        }
        private async Task<User> GetRecordAsync(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"https://localhost:7053/api/Users/GetUser?username={username}&password={password}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(content);
                }
                return new User();
            }
        }
        private async Task<List<User>> GetListAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7053/api/Users/GetList");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<User>>(content);
                }
                return new List<User>();
            }
        }
    }
}
