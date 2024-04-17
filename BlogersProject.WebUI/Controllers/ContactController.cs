using Microsoft.AspNetCore.Mvc;

namespace BlogersProject.WebUI.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
