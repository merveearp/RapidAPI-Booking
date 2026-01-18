using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Models;
using RapidAPI_BookingProject.Services.BookingServices;
using RapidAPI_BookingProject.Services.ClaudeApiServices;


public class HomeController(IBookingService _bookingService,IClaudeService _claudeService) : Controller
{
    public IActionResult Index()
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

