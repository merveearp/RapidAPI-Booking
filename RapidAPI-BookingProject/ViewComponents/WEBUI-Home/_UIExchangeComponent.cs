using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ExternalServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIExchangeComponent(IExternalService _externalService) :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var exchange = await _externalService.GetExchangeAsync();
            return View(exchange);
        }
    }
}
