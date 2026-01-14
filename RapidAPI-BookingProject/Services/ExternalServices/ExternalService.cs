using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RapidAPI_BookingProject.Dtos.ExternalDtos;

namespace RapidAPI_BookingProject.Services.ExternalServices
{
    public class ExternalService : IExternalService
    {
        private readonly string rapidapi_key = "b64f56ed1emsh8afba1e8adc4772p11a325jsn1d2ee2523f59";
        private readonly string rapidapi_host = "weather-api138.p.rapidapi.com";

        public async Task<ResultWeatherDto> GetWeatherAsync(string cityName)
        {
            var searchCity = string.IsNullOrWhiteSpace(cityName)
                ? "istanbul"
                : cityName;

            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    $"https://weather-api138.p.rapidapi.com/weather?city_name={searchCity}"
                ),
                Headers =
                {
                    { "x-rapidapi-key", rapidapi_key },
                    { "x-rapidapi-host", rapidapi_host }
                }
            };

            var response = await client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<ResultWeatherDto>(body);
            value.main.temp -= (float)273.15;
            value.main.feels_like -= (float)273.15;

            return value;


        }
    }
}
