using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RapidAPI_BookingProject.Models;
using RapidAPI_BookingProject.Services.BookingServices;
using RapidAPI_BookingProject.Services.ExternalServices;

public class HomeController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly IExternalService _externalService;

    public HomeController(IBookingService bookingService, IExternalService externalService)
    {
        _bookingService = bookingService;
        _externalService = externalService;
    }

    public async Task<IActionResult> Index()
    {
        var weather = await _externalService.GetWeatherAsync(null);
        return View(weather);
    }



    public async Task<IActionResult> SearchHotel( string cityName, string checkIn, string checkOut,int adults)
    {    
        var locations = await _bookingService.GetLocationAsync(cityName);
        var destId = locations.FirstOrDefault()?.dest_id;

        if (string.IsNullOrEmpty(destId))
        {
            return RedirectToAction("Index");
        }

        var hotels = await _bookingService.GetByHotelListAsync( destId, checkIn, checkOut, adults);

       
        ViewBag.CheckIn = checkIn;
        ViewBag.CheckOut = checkOut;

        return View("HotelList", hotels);
    }


    public async Task<IActionResult> HotelDetail(string hotelId, string checkIn, string checkOut)
    {
        var detail = await _bookingService.GetHotelDetailAsync(hotelId, checkIn,checkOut);       
        var photos = await _bookingService.GetByHotelPhotosAsync(hotelId);
        var description = await _bookingService.GetHotelDescriptionAsync(hotelId);
        var score = await _bookingService.GetByHotelScoreAsync(hotelId);

        
        var model = new HotelDetailViewModel
        {
            Detail = detail,
            Photos = photos,
            Description=description,
            Score=score,
            CheckIn = checkIn,
            CheckOut = checkOut
        };

        
        return View(model);
    }


}
