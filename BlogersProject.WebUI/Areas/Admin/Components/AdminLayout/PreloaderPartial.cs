using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebUI.Areas.Admin.Components.AdminLayout
{
    public class PreloaderPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(); 
        }
    }
}
