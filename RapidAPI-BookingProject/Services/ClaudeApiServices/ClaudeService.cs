using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RapidAPI_BookingProject.Dtos.ClaudeDtos;
using RapidAPI_BookingProject.Services.ClaudeApiServices;
using System.Text;

namespace RapidAPI_BookingProject.Services.ClaudeServices
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

                // Claude bazen ```json ... ``` ile sarar, temizle
                contentText = contentText.Replace("```json", "").Replace("```", "").Trim();

                // JSON'u parse et
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

        /// <summary>
        /// Unsplash'tan random yemek görseli al
        /// </summary>
        private string GetUnsplashImageUrl(string searchKeyword)
        {
            // Unsplash'ın ücretsiz random image API'si
            // API key gerekmez, ama rate limit var (50/saat)
            return $"https://source.unsplash.com/800x600/?{searchKeyword}";
        }

        /// <summary>
        /// Hata durumunda varsayılan yemek
        /// </summary>
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
    }
}