using RapidAPI_BookingProject.Dtos.OpenAIRotaDtos;

namespace RapidAPI_BookingProject.Services.OpenAIServices
{
    public interface IOpenAIService
    {
        Task<List<ResultPlaceDto>> GetTravelRouteAsync(string cityName);
    }
}
