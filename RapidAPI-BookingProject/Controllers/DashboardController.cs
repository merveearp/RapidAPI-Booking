using Microsoft.AspNetCore.Mvc;

namespace RapidAPI_BookingProject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
