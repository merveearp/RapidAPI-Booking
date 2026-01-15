using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ExternalServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIGoldAPIComponent(IExternalService _externalService) :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var goldPrice = await _externalService.GetGoldPriceAsync();
            return View(goldPrice);
        }
    }
}
