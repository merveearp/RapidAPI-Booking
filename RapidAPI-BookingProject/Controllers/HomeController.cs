using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.BookingServices;

public class HomeController : Controller
{
    private readonly IBookingService _bookingService;

    public HomeController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public IActionResult Index()
    {
        return View();
    }

   
    public async Task<IActionResult> SearchHotel( string cityName,string checkIn,string checkOut,int adults)
    {   
        var locations = await _bookingService.GetLocationAsync(cityName);
        var destId = locations.FirstOrDefault()?.dest_id;

        if (string.IsNullOrEmpty(destId))
        {
            return RedirectToAction("Index");
        }

        
        var hotels = await _bookingService.GetByHotelListAsync( destId, checkIn,checkOut, adults );

        return View("HotelList", hotels);
    }

    public async Task<IActionResult> HotelDetail(string hotelId)
    {
        var value = await _bookingService.GetHotelDetailAsync(hotelId);
        return View(value);
    }
}
