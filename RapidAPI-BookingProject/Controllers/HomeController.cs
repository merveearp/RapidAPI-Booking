using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RapidAPI_BookingProject.Dtos.BookingDtos;
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
        return View();
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

