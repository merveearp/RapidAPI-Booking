using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ExternalServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Layout
{
    public class _LayoutHeaderComponent(IExternalService _externalService) :ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var weather = await _externalService.GetWeatherAsync(null); 
            return View(weather);
        }
    }
}
