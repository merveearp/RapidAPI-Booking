using Microsoft.AspNetCore.Mvc;

namespace RapidAPI_BookingProject.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RoomDetail()
        {
            return View();
        }
    }
}
