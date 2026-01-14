using RapidAPI_BookingProject.Dtos.ExternalDtos;

namespace RapidAPI_BookingProject.Services.ExternalServices
{
    public interface IExternalService
    {
        Task<ResultWeatherDto> GetWeatherAsync(string cityName);
    }
}
