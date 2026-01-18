using RapidAPI_BookingProject.Dtos.ExternalDtos;

namespace RapidAPI_BookingProject.Services.ExternalServices
{
    public interface IExternalService
    {
        Task<ResultWeatherDto> GetWeatherAsync(string cityName);
        Task<ResultExchangeDto> GetExchangeAsync();
        Task<ResultCryptoDto> GetCryptoAsync();
        Task<ResultGoldPriceDto> GetGoldPriceAsync();
        Task<ResultNewsOfTurkeyDto> GetNewsOfTurkeyAsync();
        Task<List<ResultImdbMovieDto>> GetMovieAsync();

    }
}
