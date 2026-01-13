using Microsoft.AspNetCore.Mvc;

namespace RapidAPI_BookingProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
