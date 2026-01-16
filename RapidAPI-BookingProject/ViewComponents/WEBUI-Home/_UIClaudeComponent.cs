using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ClaudeApiServices;
using RapidAPI_BookingProject.Services.ClaudeServices;

namespace RapidAPI_BookingProject.ViewComponents
{
    public class _UIClaudeComponent : ViewComponent
    {
        private readonly IClaudeService _claudeService;

        public _UIClaudeComponent(IClaudeService claudeService)
        {
            _claudeService = claudeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var dailyDish = await _claudeService.GetDailyDishAsync();
            return View(dailyDish);
        }
    }
}