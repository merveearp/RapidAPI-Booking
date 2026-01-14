using Newtonsoft.Json;
using RapidAPI_BookingProject.Dtos.ExternalDtos;

namespace RapidAPI_BookingProject.Services.ExternalServices
{
    public class ExternalService : IExternalService
    {
        private readonly string rapidapi_key = "b64f56ed1emsh8afba1e8adc4772p11a325jsn1d2ee2523f59";
        private readonly string rapidapi_host = "open-weather13.p.rapidapi.com";

        public async Task<ResultWeatherDto> GetWeatherAsync(string cityName)
        {

            var searchCity = string.IsNullOrWhiteSpace(cityName)
                ? "istanbul"
                : cityName;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://open-weather13.p.rapidapi.com/city?city={searchCity}&lang=TR"),
                Headers =
                {
                    { "x-rapidapi-key", rapidapi_key},
                    { "x-rapidapi-host", rapidapi_host },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultWeatherDto>(body);
                return value;
               
            }
        }
    }
}
