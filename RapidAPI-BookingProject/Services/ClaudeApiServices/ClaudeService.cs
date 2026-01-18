using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RapidAPI_BookingProject.Dtos.ClaudeDtos;
using System.Text;

namespace RapidAPI_BookingProject.Services.ClaudeApiServices
{
    public class ClaudeService : IClaudeService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl = "https://api.anthropic.com/v1/messages";

        public ClaudeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DailyDishDto> GetDailyDishAsync()
        {
            try
            {

                var apiKey = _configuration["AnthropicApiKey"];

                if (string.IsNullOrEmpty(apiKey))
                {
                    Console.WriteLine("API Key bulunamadı!");
                    return GetDefaultDish();
                }

                var client = new HttpClient();


                var prompt = @"Bana bugün için ilgi çekici, kültürel bir yemek önerisi ver.

                Cevabı sadece JSON formatında ver, başka hiçbir şey yazma:
                {
                  ""dishName"": ""Yemek adı (Türkçe)"",
                  ""culturalTheme"": ""Kültürel tema (örn: Akdeniz Esintisi)"",
                  ""description"": ""Kısa açıklama (1, 2 cümle ama içerik malzemeleri de 1 cümleyle ekle )"",
                  ""emoji"": ""Uygun bir emoji"",
                  ""searchKeyword"": ""Unsplash için İngilizce arama kelimesi (örn: grilled salmon dish)""
                }";


                var requestBody = new
                {
                    model = "claude-sonnet-4-20250514",
                    max_tokens = 500,
                    messages = new[]
                    {
                        new
                        {
                            role = "user",
                            content = prompt
                        }
                    }
                };

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_apiUrl),
                    Content = content,
                    Headers =
                    {
                        { "x-api-key", apiKey },
                        { "anthropic-version", "2023-06-01" }
                    }
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();


                    return ParseClaudeResponse(body);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Claude API Hatası: {ex.Message}");
                return GetDefaultDish();
            }
        }


        private DailyDishDto ParseClaudeResponse(string responseBody)
        {
            try
            {
                var jsonResponse = JObject.Parse(responseBody);


                var contentText = jsonResponse["content"]?[0]?["text"]?.ToString();

                if (string.IsNullOrEmpty(contentText))
                {
                    return GetDefaultDish();
                }


                contentText = contentText.Replace("```json", "").Replace("```", "").Trim();


                var dishData = JObject.Parse(contentText);

                return new DailyDishDto
                {
                    DishName = dishData["dishName"]?.ToString() ?? "Fırında Somon",
                    CulturalTheme = dishData["culturalTheme"]?.ToString() ?? "Akdeniz Esintisi",
                    Description = dishData["description"]?.ToString() ?? "Lezzetli bir yemek önerisi",
                    Emoji = dishData["emoji"]?.ToString() ?? "🍽️",
                    ImageUrl = GetUnsplashImageUrl(dishData["searchKeyword"]?.ToString() ?? "food")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Parse Hatası: {ex.Message}");
                return GetDefaultDish();
            }
        }

        private string GetUnsplashImageUrl(string searchKeyword)
        {

            return $"https://source.unsplash.com/800x600/?{searchKeyword}";
        }


        private DailyDishDto GetDefaultDish()
        {
            return new DailyDishDto
            {
                DishName = "Fırında Somon",
                CulturalTheme = "Akdeniz Esintisi",
                Description = "Zeytinyağı ve limon ile marine edilmiş taze somon, yanında taze sebzeler.",
                Emoji = "🐟",
                ImageUrl = "https://source.unsplash.com/800x600/?grilled-salmon"
            };
        }

        public async Task<List<ResultPlaceDto>> GetPlacesAsync(string cityName)
        {
            try
            {
                var apiKey = _configuration["AnthropicApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    Console.WriteLine("API Key bulunamadı!");
                    return GetDefaultPlaces(cityName);
                }

                using var client = new HttpClient();

                var prompt = $@"
                {cityName} şehri için gezilmesi gereken en popüler 4 tarihi ve kültürel yeri listele.

                Cevabı SADECE JSON formatında ver, başka hiçbir şey yazma:
                [
                  {{
                    ""place"": ""Gezilecek yerin adı (Türkçe)"",
                    ""description"": ""Kısa açıklama (2-3 cümle: nerede olduğu, ne zaman yapıldığı, neden önemli)"",
                    ""searchKeyword"": ""Unsplash için İngilizce arama kelimesi (örn: Hagia Sophia Istanbul)""
                  }}
                ]";

                var requestBody = new
                {
                    model = "claude-sonnet-4-20250514",
                    max_tokens = 1000,
                    messages = new[]
                    {
                new
                {
                    role = "user",
                    content = prompt
                }
            }
                };

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_apiUrl),
                    Content = content,
                    Headers =
            {
                { "x-api-key", apiKey },
                { "anthropic-version", "2023-06-01" }
            }
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    return ParsePlacesResponse(body);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Claude API Hatası: {ex.Message}");
                return GetDefaultPlaces(cityName);
            }
        }

        private List<ResultPlaceDto> ParsePlacesResponse(string responseBody)
        {
            try
            {
                var jsonResponse = JObject.Parse(responseBody);
                var contentText = jsonResponse["content"]?[0]?["text"]?.ToString();

                if (string.IsNullOrEmpty(contentText))
                {
                    return new List<ResultPlaceDto>();
                }

                // Claude bazen ```json ile sarmalayabilir, temizle
                contentText = contentText.Replace("```json", "").Replace("```", "").Trim();

                var placesData = JArray.Parse(contentText);
                var places = new List<ResultPlaceDto>();

                foreach (var item in placesData)
                {
                    places.Add(new ResultPlaceDto
                    {
                        Place = item["place"]?.ToString() ?? "Bilinmeyen Yer",
                        Description = item["description"]?.ToString() ?? "Açıklama bulunamadı",
                        ImageUrl = GetUnsplashImageUrl(item["searchKeyword"]?.ToString() ?? "landmark")
                    });
                }

                return places;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Parse Hatası: {ex.Message}");
                return new List<ResultPlaceDto>();
            }
        }
     
        private List<ResultPlaceDto> GetDefaultPlaces(string cityName)
        {
           
            return new List<ResultPlaceDto>
    {
        new ResultPlaceDto
        {
            Place = "Ayasofya Camii",
            Description = "Sultanahmet'te bulunan, 537 yılında inşa edilmiş muhteşem tarihi yapı. Bizans ve Osmanlı mimarisinin en önemli örneklerinden biri.",
            ImageUrl = "https://source.unsplash.com/800x600/?hagia-sophia-istanbul"
        },
        new ResultPlaceDto
        {
            Place = "Topkapı Sarayı",
            Description = "Eminönü'nde yer alan, Osmanlı padişahlarının 400 yıl boyunca yaşadığı saray. Muhteşem Boğaz manzarası ve tarihi eserleriyle ünlü.",
            ImageUrl = "https://source.unsplash.com/800x600/?topkapi-palace"
        },
        new ResultPlaceDto
        {
            Place = "Kapalıçarşı",
            Description = "1461 yılında kurulan, dünyanın en eski ve en büyük kapalı çarşılarından biri. 4000'den fazla dükkânıyla alışveriş cenneti.",
            ImageUrl = "https://source.unsplash.com/800x600/?grand-bazaar-istanbul"
        },
        new ResultPlaceDto
        {
            Place = "Galata Kulesi",
            Description = "Beyoğlu'nda bulunan, 1348 yılında Cenevizliler tarafından inşa edilmiş kule. İstanbul'un panoramik manzarasını sunar.",
            ImageUrl = "https://source.unsplash.com/800x600/?galata-tower"
        }
    };
        }

    }
}
