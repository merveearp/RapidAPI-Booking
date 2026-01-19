using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ClaudeApiServices;
using RapidAPI_BookingProject.Services.MealServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIMealComponent(IMealService _mealService,IClaudeService _claudeService) :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var meal = await _mealService.GetMealAsync();

            if (meal != null)
            {
                meal.strInstructions =
                    await _claudeService.GenerateTurkishMealDescriptionAsync(
                        meal.strMeal,
                        meal.strInstructions
                    );
            }

            return View(meal);
        }
    }
}
