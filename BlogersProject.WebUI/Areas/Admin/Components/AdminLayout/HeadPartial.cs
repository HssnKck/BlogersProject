using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebUI.Areas.Admin.Components.AdminLayout
{
    public class HeadPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(); 
        }
    }
}
