using RapidAPI_BookingProject.Dtos.ClaudeDtos;

namespace RapidAPI_BookingProject.Services.ClaudeApiServices
{
    public interface IClaudeService
    {
        Task<DailyDishDto> GetDailyDishAsync();
    }
}
