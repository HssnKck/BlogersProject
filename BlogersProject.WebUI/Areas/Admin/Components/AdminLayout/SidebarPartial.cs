using BlogersProject.Model.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebUI.Areas.Admin.Components.AdminLayout
{
    public class SidebarPartial : ViewComponent 
    {
        private readonly Context _db;

        public SidebarPartial(Context db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(); 
        }
    }
}
