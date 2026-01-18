using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ClaudeApiServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIClaudeRotaComponent(IClaudeService _claudeService) : ViewComponent
    {
       
        public async Task<IViewComponentResult> InvokeAsync(string cityName = null)
        {
           
            cityName = string.IsNullOrWhiteSpace(cityName)
                ? HttpContext.Request.Query["cityName"].ToString()
                : cityName;

            cityName = string.IsNullOrWhiteSpace(cityName) ? "İstanbul" : cityName;

            ViewBag.CityName = cityName;

            var places = await _claudeService.GetPlacesAsync(cityName);
            return View(places);
        }
    }
}