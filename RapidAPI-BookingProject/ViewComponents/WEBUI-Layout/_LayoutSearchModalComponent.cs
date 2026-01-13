using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.BookingServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Layout
{
    public class _LayoutSearchModalComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
