using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ExternalServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UINewsAPIComponent(IExternalService _externalService) :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var news = await _externalService.GetNewsOfTurkeyAsync();
            return View(news);
        }
    }
}
