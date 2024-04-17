using BlogersProject.Model.Concrete;
using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogersProject.WebUI.Components.Content
{
    public class ContentCommentsPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
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
					return View("Eror");
				}
			}
        }
    }
}
