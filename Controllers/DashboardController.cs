using Microsoft.AspNetCore.Mvc;

namespace SiteMVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
