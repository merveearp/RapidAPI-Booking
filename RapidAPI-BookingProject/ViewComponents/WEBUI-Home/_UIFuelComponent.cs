using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.FuelServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIFuelComponent(IFuelService _fuelService) : ViewComponent
    {
       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var fuelPrice = await _fuelService.GetFuelPricesAsync();
            return View(fuelPrice);
        }
    }
}
