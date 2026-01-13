using Microsoft.AspNetCore.Mvc;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UITestimonialComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
