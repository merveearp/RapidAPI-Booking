using RapidAPI_BookingProject.Dtos.ClaudeDtos;

namespace RapidAPI_BookingProject.Services.OpenAIServices
{
    public interface IOpenAIService
    {
        Task<List<ResultPlaceDto>> GetTravelRouteAsync(string cityName);
    }
}
