using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.BookingServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIHotelListComponent : ViewComponent
    {
        private readonly IBookingService _bookingService;

        public _UIHotelListComponent(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IViewComponentResult> InvokeAsync( string cityName,string checkIn, string checkOut,int adults)
        {
            
            cityName = string.IsNullOrWhiteSpace(cityName)
                ? HttpContext.Request.Query["cityName"].ToString()
                : cityName;

            checkIn = string.IsNullOrWhiteSpace(checkIn)
                ? HttpContext.Request.Query["checkIn"].ToString()
                : checkIn;

            checkOut = string.IsNullOrWhiteSpace(checkOut)
                ? HttpContext.Request.Query["checkOut"].ToString()
                : checkOut;

            adults = adults <= 0 && int.TryParse(
                HttpContext.Request.Query["adults"], out var a)
                ? a
                : adults;

           
            cityName = string.IsNullOrWhiteSpace(cityName) ? "istanbul" : cityName;
            checkIn = string.IsNullOrWhiteSpace(checkIn)
                ? DateTime.Today.ToString("yyyy-MM-dd")
                : checkIn;
            checkOut = string.IsNullOrWhiteSpace(checkOut)
                ? DateTime.Today.AddDays(3).ToString("yyyy-MM-dd")
                : checkOut;
            adults = adults <= 0 ? 2 : adults;

            var locations = await _bookingService.GetLocationAsync(cityName);
            var destId = locations.FirstOrDefault()?.dest_id;

            var hotels = await _bookingService
                .GetByHotelListAsync(destId, checkIn, checkOut, adults);

            ViewBag.CheckIn = checkIn;
            ViewBag.CheckOut = checkOut;

            return View(hotels);
        }

    }
}
