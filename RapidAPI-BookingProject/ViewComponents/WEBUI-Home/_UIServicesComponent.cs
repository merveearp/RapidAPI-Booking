using Microsoft.AspNetCore.Mvc;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIServicesComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
