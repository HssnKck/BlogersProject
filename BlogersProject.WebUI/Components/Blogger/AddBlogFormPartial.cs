using BlogersProject.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogersProject.WebUI.Components.Blogger
{
    public class AddBlogFormPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new Blog());
        }
    }
}
