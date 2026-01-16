using RapidAPI_BookingProject.Services.BookingServices;
using RapidAPI_BookingProject.Services.ClaudeApiServices;
using RapidAPI_BookingProject.Services.ClaudeServices;
using RapidAPI_BookingProject.Services.ExternalServices;
using System.Reflection;

namespace RapidAPI_BookingProject.Extensions
{
    public static class ServiceRegistration
    {
        public static void ConfigureService(this IServiceCollection services)
        {
           
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IExternalService, ExternalService>();
            services.AddScoped<IClaudeService, ClaudeService>();
        }
    }
}
