using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebUI.Areas.Admin.Components.AdminLayout
{
    public class HeaderPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
