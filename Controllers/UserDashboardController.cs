using Microsoft.AspNetCore.Mvc;

namespace LMS_UI.Controllers
{
    public class UserDashboardController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Book");
        }
    }
}
