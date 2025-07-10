using Microsoft.AspNetCore.Mvc;

namespace LMS_UI.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
