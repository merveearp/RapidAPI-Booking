using RapidAPI_BookingProject.Dtos.MealDtos;

namespace RapidAPI_BookingProject.Services.MealServices
{
    public interface IMealService
    {
        Task<TheMealDbResponse.Meal?> GetMealAsync();
    }
}
