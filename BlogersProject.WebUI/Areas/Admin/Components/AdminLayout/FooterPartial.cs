using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebUI.Areas.Admin.Components.AdminLayout
{
    public class FooterPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
