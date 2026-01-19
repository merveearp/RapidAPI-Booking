using Newtonsoft.Json;
using RapidAPI_BookingProject.Dtos.MealDtos;

namespace RapidAPI_BookingProject.Services.MealServices
{
    public class MealService : IMealService
    {
       
        public async Task<TheMealDbResponse.Meal?> GetMealAsync()
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(
                    "https://www.themealdb.com/api/json/v1/1/random.php");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<TheMealDbResponse>(json);

                return data?.meals?.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
