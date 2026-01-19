using RapidAPI_BookingProject.Services.BookingServices;
using RapidAPI_BookingProject.Services.ClaudeApiServices;
using RapidAPI_BookingProject.Services.ExternalServices;
using RapidAPI_BookingProject.Services.FuelServices;
using RapidAPI_BookingProject.Services.MealServices;
using RapidAPI_BookingProject.Services.OpenAIServices;


namespace RapidAPI_BookingProject.Extensions
{
    public static class ServiceRegistration
    {
        public static void ConfigureService(this IServiceCollection services)
        {
           
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IExternalService, ExternalService>();
            services.AddScoped<IClaudeService, ClaudeService>();
            services.AddScoped<IFuelService, FuelService>();
            services.AddScoped<IOpenAIService, OpenAIService>();
            services.AddScoped<IMealService, MealService>();



        }
    }
}
