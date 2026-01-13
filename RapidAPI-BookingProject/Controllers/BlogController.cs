using Microsoft.AspNetCore.Mvc;

namespace RapidAPI_BookingProject.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BlogDetail()
        {
            return View();
        }
    }
}
