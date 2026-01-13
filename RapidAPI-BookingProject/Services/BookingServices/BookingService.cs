

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RapidAPI_BookingProject.Dtos;

namespace RapidAPI_BookingProject.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        private readonly string rapidapi_key = "b64f56ed1emsh8afba1e8adc4772p11a325jsn1d2ee2523f59";
        private readonly string rapidapi_host = "booking-com.p.rapidapi.com";

        public async Task<List<SearchLocationDto>> GetLocationAsync(string cityName)
        {
            var searchCity = string.IsNullOrWhiteSpace(cityName)
                ? "Istanbul"
                : cityName;

            using var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    $"https://booking-com.p.rapidapi.com/v1/hotels/locations?name={searchCity}&locale=en-gb"),
            };

            request.Headers.Add("x-rapidapi-key", rapidapi_key);
            request.Headers.Add("x-rapidapi-host", rapidapi_host);

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<SearchLocationDto>>(body);

            return values?
                .Take(1)
                .ToList()
                ?? new List<SearchLocationDto>();
        }

        // 🔹 OTEL LİSTESİ
        public async Task<List<ResultHotelListDto>> GetByHotelListAsync(
            string destId,
            string checkIn,
            string checkOut,
            int adults)
        {
            var finalDestId = string.IsNullOrWhiteSpace(destId)
                ? "-755070"
                : destId;

            var finalCheckIn = string.IsNullOrWhiteSpace(checkIn)
                ? DateTime.Today.ToString("yyyy-MM-dd")
                : checkIn;

            var finalCheckOut = string.IsNullOrWhiteSpace(checkOut)
                ? DateTime.Parse(finalCheckIn).AddDays(3).ToString("yyyy-MM-dd")
                : checkOut;

            var finalAdults = adults > 0 ? adults : 2;

            var url =
                $"https://booking-com.p.rapidapi.com/v2/hotels/search" +
                $"?checkout_date={finalCheckOut}" +
                $"&checkin_date={finalCheckIn}" +
                $"&dest_id={finalDestId}" +
                $"&dest_type=city" +
                $"&order_by=popularity" +
                $"&filter_by_currency=EUR" +
                $"&adults_number={finalAdults}" +
                $"&room_number=1" +
                $"&units=metric" +
                $"&locale=en-gb" +
                $"&page_number=0";

            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("x-rapidapi-key", rapidapi_key);
            request.Headers.Add("x-rapidapi-host", rapidapi_host);

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<HotelSearchResponseDto>(body);

            return result?.results ?? new List<ResultHotelListDto>();
        }

       
        public async Task<ResultHotelDetailDto> GetHotelDetailAsync(string hotelId)
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    $"https://booking-com.p.rapidapi.com/v1/hotels/data" +
                    $"?hotel_id={hotelId}&locale=en-gb"
                ),
            };

            request.Headers.Add("x-rapidapi-key", rapidapi_key);
            request.Headers.Add("x-rapidapi-host", rapidapi_host);

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var value = JsonConvert.DeserializeObject<ResultHotelDetailDto>(body);

            return value;
        }
    }

}







