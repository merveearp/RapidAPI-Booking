using Newtonsoft.Json;
using RapidAPI_BookingProject.Dtos.FuelDtos;

namespace RapidAPI_BookingProject.Services.FuelServices
{
    public class FuelService : IFuelService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FuelService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<OpetFuelResponseDto> GetFuelPricesAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "https://www.opet.com.tr/api/fuelprice?slug=istanbul-avrupa"
            );

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Channel", "Web");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return new OpetFuelResponseDto
                {
                    data = new List<ProvinceFuelDto>()
                };
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OpetFuelResponseDto>(json);

            return result ?? new OpetFuelResponseDto
            {
                data = new List<ProvinceFuelDto>()
            };
        }
    }
}
