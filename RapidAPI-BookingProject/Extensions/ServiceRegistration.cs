using RapidAPI_BookingProject.Services.BookingServices;
using System.Reflection;

namespace RapidAPI_BookingProject.Extensions
{
    public static class ServiceRegistration
    {
        public static void ConfigureService(this IServiceCollection services)
        {
           
            services.AddScoped<IBookingService, BookingService>();
        }
    }
}
