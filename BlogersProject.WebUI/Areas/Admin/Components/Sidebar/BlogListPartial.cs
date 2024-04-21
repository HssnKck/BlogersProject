using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogersProject.WebUI.Areas.Admin.Components.Sidebar
{
    public class BlogListPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
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
                    return View("Eror");
                }
            }
        }
    }
}
