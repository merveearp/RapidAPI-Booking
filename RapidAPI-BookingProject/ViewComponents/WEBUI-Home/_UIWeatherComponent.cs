using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ExternalServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIWeatherComponent(IExternalService _externalService) :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string cityName)
        {
            cityName = string.IsNullOrWhiteSpace(cityName)
               ? HttpContext.Request.Query["cityName"].ToString()
               : cityName;

            cityName = string.IsNullOrWhiteSpace(cityName)
                ? "istanbul"
                : cityName;

            var weather = await _externalService.GetWeatherAsync(cityName);
            return View(weather);
        }
    }
}
