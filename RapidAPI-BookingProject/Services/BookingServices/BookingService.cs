

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RapidAPI_BookingProject.Dtos.BookingDtos;
using static System.Net.WebRequestMethods;

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

       
        public async Task<List<ResultHotelListDto>> GetByHotelListAsync( string destId, string checkIn, string checkOut, int adults)
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
                $"&locale=tr" +
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


        public async Task<ResultHotelDetailDto> GetHotelDetailAsync( string hotelId, string checkIn, string checkOut)
        {
            var finalCheckIn = string.IsNullOrWhiteSpace(checkIn)
                ? DateTime.Today.ToString("yyyy-MM-dd")
                : checkIn;

            var finalCheckOut = string.IsNullOrWhiteSpace(checkOut)
                ? DateTime.Parse(finalCheckIn).AddDays(3).ToString("yyyy-MM-dd")
                : checkOut;

            var url =
                $"https://booking-com.p.rapidapi.com/v2/hotels/details" +
                $"?hotel_id={hotelId}" +
                $"&checkin_date={finalCheckIn}" +
                $"&checkout_date={finalCheckOut}" +
                $"&locale=tr" +
                $"&currency=EUR";

            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("x-rapidapi-key", rapidapi_key);
            request.Headers.Add("x-rapidapi-host", rapidapi_host);

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultHotelDetailDto>(body);

            return result;
        }

        public async Task<List<ResultByHotelPhotosDto>> GetByHotelPhotosAsync(string hotelId)
        {
            var url =
                $"https://booking-com.p.rapidapi.com/v1/hotels/photos" +
                $"?hotel_id={hotelId}" +
                $"&locale=en-gb";

            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("x-rapidapi-key", rapidapi_key);
            request.Headers.Add("x-rapidapi-host", rapidapi_host);

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var photos = JsonConvert.DeserializeObject<List<ResultByHotelPhotosDto>>(body);

            return photos?
                .Take(10)
                .ToList()
                ?? new List<ResultByHotelPhotosDto>();
        }

        public async Task<ResultHotelDescriptionDto> GetHotelDescriptionAsync(string hotelId)
        {


            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v2/hotels/description?hotel_id={hotelId}&locale=tr"),
                Headers =
            {
                { "x-rapidapi-key", rapidapi_key },
                { "x-rapidapi-host", rapidapi_host },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultHotelDescriptionDto>(body);
                return value;
            }
        }

        public async Task<ResultByHotelScoreDto> GetByHotelScoreAsync(string hotelId)
        {

            var url = $"https://booking-com.p.rapidapi.com/v1/hotels/review-scores?hotel_id={hotelId}&locale=tr";

            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("x-rapidapi-key", rapidapi_key);
            request.Headers.Add("x-rapidapi-host", rapidapi_host);

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultByHotelScoreDto>(body);

            return result;

        }
    }

}

//b64f56ed1emsh8afba1e8adc4772p11a325jsn1d2ee2523f59