using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RapidAPI_BookingProject.Dtos.ExternalDtos;

namespace RapidAPI_BookingProject.Services.ExternalServices
{
    public class ExternalService : IExternalService
    {
        private readonly string rapidapi_key = "b64f56ed1emsh8afba1e8adc4772p11a325jsn1d2ee2523f59";
        private readonly string rapidapi_hostweather = "weather-api138.p.rapidapi.com";
        private readonly string rapidapi_hostexchange = "exchangerate-api.p.rapidapi.com";
        private readonly string rapidapi_hostcrypto = "fast-price-exchange-rates.p.rapidapi.com";
        private readonly string rapidapi_gold = "harem-altin-live-gold-price-data.p.rapidapi.com";
        private readonly string rapidapi_news = "real-time-news-data.p.rapidapi.com";
        private readonly string rapidapi_movie = "imdb-top-1002.p.rapidapi.com";

        public async Task<ResultCryptoDto> GetCryptoAsync()
        {
           
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://fast-price-exchange-rates.p.rapidapi.com/api/v1/convert-rates/crypto/from?detailed=false&currency=USD"),
                Headers =
                {
                    { "x-rapidapi-key", rapidapi_key },
                    { "x-rapidapi-host", rapidapi_hostcrypto },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultCryptoDto>(body);
                return value;
            }
        }

        public async Task<ResultExchangeDto> GetExchangeAsync()
        {
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://exchangerate-api.p.rapidapi.com/rapid/latest/try"),
                Headers =
                {
                    { "x-rapidapi-key", rapidapi_key },
                    { "x-rapidapi-host", rapidapi_hostexchange},
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultExchangeDto>(body);
                return value;

            }
        }

        public async Task<ResultGoldPriceDto> GetGoldPriceAsync()
        {
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://harem-altin-live-gold-price-data.p.rapidapi.com/harem_altin/prices/23b4c2fb31a242d1eebc0df9b9b65e5e"),
                Headers =
                {
                    { "x-rapidapi-key", rapidapi_key },
                    { "x-rapidapi-host", rapidapi_gold },
                },
                        };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultGoldPriceDto>(body);
                
                var allowedKeys = new List<string>
                {
                    "14 AYAR",
                    "22 AYAR",
                    "ALTIN GÜMÜŞ",
                    "GRAM ALTIN",
                    "GÜMÜŞ TL",
                    "YENİ ÇEYREK"
                };

               
                value.data = value.data
                    .Where(x => allowedKeys.Contains(x.key))
                    .ToArray();

                return value;
            }
        }

        public async Task<List<ResultImdbMovieDto>> GetMovieAsync()
        {
          
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-1002.p.rapidapi.com/top_100_movies"),
                Headers =
                    {
                        { "x-rapidapi-key", rapidapi_key },
                        { "x-rapidapi-host", rapidapi_movie},
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultImdbMovieDto>>(body);
                return values;
            }
        }

        public async Task<ResultNewsOfTurkeyDto> GetNewsOfTurkeyAsync()
        {
           
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-news-data.p.rapidapi.com/search?query=turkey%20finance&limit=8&time_published=anytime&country=TR&lang=tr"),
                Headers =
            {
                { "x-rapidapi-key", rapidapi_key },
                { "x-rapidapi-host", rapidapi_news },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultNewsOfTurkeyDto>(body);
                return value;
            }
        }

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
                    $"https://weather-api138.p.rapidapi.com/weather?city_name={searchCity}&lang=tr"
                ),
                Headers =
                {
                    { "x-rapidapi-key", rapidapi_key },
                    { "x-rapidapi-host", rapidapi_hostweather }
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
