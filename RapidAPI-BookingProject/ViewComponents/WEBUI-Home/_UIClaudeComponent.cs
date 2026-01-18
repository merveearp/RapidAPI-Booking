using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ClaudeApiServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIClaudeComponent(IClaudeService _claudeService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dailyDish = await _claudeService.GetDailyDishAsync();
            return View(dailyDish);
        }
    }
}