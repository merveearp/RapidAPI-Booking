using Microsoft.AspNetCore.Mvc;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIWorldClockComponent :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
