

using RapidAPI_BookingProject.Dtos;

namespace RapidAPI_BookingProject.Services.BookingServices
{
    public interface IBookingService
    {
        Task<List<SearchLocationDto>> GetLocationAsync(string cityName);
        Task<List<ResultHotelListDto>> GetByHotelListAsync(string destId, string checkIn, string checkOut, int adults);
        Task<ResultHotelDetailDto> GetHotelDetailAsync(string hotelId,string checkIn,string checkOut);
        Task<List<ResultByHotelPhotosDto>> GetByHotelPhotosAsync(string hotelId);


    }
}
