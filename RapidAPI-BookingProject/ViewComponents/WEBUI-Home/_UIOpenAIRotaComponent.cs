using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.OpenAIServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIOpenAIRotaComponent : ViewComponent
    {
        private readonly IOpenAIService _openAIService;

        public _UIOpenAIRotaComponent(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string cityName)
        {
            cityName = string.IsNullOrWhiteSpace(cityName)
                ? HttpContext.Request.Query["cityName"].ToString()
                : cityName;

            if (string.IsNullOrWhiteSpace(cityName))
                cityName = "istanbul";

            ViewBag.CityName = cityName;

            var places = await _openAIService.GetTravelRouteAsync(cityName);
            return View(places);
        }
    }

}
