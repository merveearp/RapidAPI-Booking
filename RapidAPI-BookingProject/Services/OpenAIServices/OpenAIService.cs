using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using RapidAPI_BookingProject.Dtos.OpenAIRotaDtos;

namespace RapidAPI_BookingProject.Services.OpenAIServices
{
    public class OpenAIService : IOpenAIService
    {
        private readonly IConfiguration _configuration;
       

        public OpenAIService(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }

        public async Task<List<ResultPlaceDto>> GetTravelRouteAsync(string cityName)
        {
            try
            {
                using var client = new HttpClient();
                var apiKey = _configuration["OpenAIKey"];
                if (string.IsNullOrWhiteSpace(apiKey))
                    return GetDefaultPlaces(cityName);

                client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", apiKey);

                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                            content = "You must return ONLY valid JSON. Do not add explanations."
                        },
                        new
                        {
                            role = "user",
                            content = $@"
                    {cityName} şehri için gezilmesi gereken EN POPÜLER 6 TARİHİ ve TURİSTİK YERİ listele.

                    Kurallar:
                    - Sadece gerçek ve bilinen yer isimleri yaz.
                    - 'Şehir merkezi' gibi belirsiz ifadeler kullanma.
                    - Tam olarak 6 farklı yer yaz.
                    - Açıklamalar kısa olsun (1-2 cümle).

                    Cevabı SADECE aşağıdaki JSON formatında ver:
                    [
                      {{
                        ""Place"": ""Yer adı"",
                        ""Description"": ""Kısa açıklama""
                      }}
                    ]"
                        }
                    }
                };

                var content = new StringContent(
                    JsonConvert.SerializeObject(requestBody),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync( "https://api.openai.com/v1/chat/completions",content);

                if (!response.IsSuccessStatusCode)
                    return GetDefaultPlaces(cityName);

                var responseString = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(responseString);

                string rawJson = data.choices[0].message.content;

               
                rawJson = rawJson.Trim();
                if (!rawJson.StartsWith("["))
                {
                    var start = rawJson.IndexOf('[');
                    var end = rawJson.LastIndexOf(']');
                    if (start >= 0 && end > start)
                        rawJson = rawJson.Substring(start, end - start + 1);
                }

                return JsonConvert.DeserializeObject<List<ResultPlaceDto>>(rawJson)
                       ?? GetDefaultPlaces(cityName);
            }
            catch
            {
                return GetDefaultPlaces(cityName);
            }
        }

        private List<ResultPlaceDto> GetDefaultPlaces(string cityName)
        {
            if (cityName.ToLower() == "istanbul")
            {
                return new List<ResultPlaceDto>
                {
                    new() { Place = "Ayasofya", Description = "Bizans ve Osmanlı dönemlerinden izler taşıyan eşsiz yapı." },
                    new() { Place = "Topkapı Sarayı", Description = "Osmanlı padişahlarının yaşadığı tarihi saray kompleksi." },
                    new() { Place = "Sultanahmet Camii", Description = "Altı minaresiyle İstanbul'un simgelerinden biri." },
                    new() { Place = "Galata Kulesi", Description = "Şehri panoramik olarak görebileceğiniz tarihi kule." },
                    new() { Place = "Kapalıçarşı", Description = "Dünyanın en eski ve en büyük kapalı çarşılarından biri." },
                    new() { Place = "Yerebatan Sarnıcı", Description = "Bizans döneminden kalma etkileyici yer altı su sarnıcı." }
                };
            }


            return new List<ResultPlaceDto>
            {
                new()
                {
                    Place = "Tarihi Merkez",
                    Description = $"{cityName} şehrinin en çok ziyaret edilen tarihi bölgesi."
                }
            };
        }
    }
}
