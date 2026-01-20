
namespace RapidAPI_BookingProject.Services.ClaudeApiServices
{
    public interface IClaudeService
    {
        Task<string> GenerateTurkishMealDescriptionAsync( string mealName,string englishInstruction );

    }
}
