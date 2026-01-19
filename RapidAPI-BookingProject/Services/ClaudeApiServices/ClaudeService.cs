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

        public async Task<string> GenerateTurkishMealDescriptionAsync( string mealName, string englishInstruction)
        {
            try
            {
                var apiKey = _configuration["AnthropicApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                    return "Lezzetli ve özenle hazırlanmış özel bir yemek.";

                var prompt = $@"
                    '{mealName}' adlı yemek için Türkçe, kısa ve iştah açıcı bir tanıtım yaz.
                    Tarif detayı verme.Malzeme verebilirsin.
                    1–3 cümle olsun.
                    Kullanıcıya hitap etsin.

                    İngilizce açıklama (referans):
                    {englishInstruction}
                    ";

                var requestBody = new
                {
                    model = "claude-sonnet-4-20250514",
                    max_tokens = 150,
                    messages = new[]
                    {
                new
                {
                    role = "user",
                    content = prompt
                }
            }
                };

                using var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, _apiUrl);
                request.Headers.Add("x-api-key", apiKey);
                request.Headers.Add("anthropic-version", "2023-06-01");
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(requestBody),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);

                return json["content"]?[0]?["text"]?.ToString()?.Trim()
                       ?? "Günün menüsünde öne çıkan, lezzetiyle dikkat çeken özel bir yemek.";
            }
            catch
            {
                return "Özenle seçilmiş, damak tadına hitap eden özel bir lezzet.";
            }
        }

    }
}
