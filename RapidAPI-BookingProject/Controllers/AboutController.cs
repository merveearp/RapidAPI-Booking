using Microsoft.AspNetCore.Mvc;

namespace RapidAPI_BookingProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
