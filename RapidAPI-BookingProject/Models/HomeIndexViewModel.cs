using RapidAPI_BookingProject.Dtos.BookingDtos;
using RapidAPI_BookingProject.Dtos.ExternalDtos;

namespace RapidAPI_BookingProject.Models
{
    public class HomeIndexViewModel
    {
        public ResultWeatherDto Weather { get; set; }
        public List<ResultHotelListDto> Hotels { get; set; }
        public ResultHotelDetailDto Detail { get; set; }
        public ResultHotelDescriptionDto Description { get; set; }
        public ResultByHotelScoreDto Score { get; set; }
        public List<ResultByHotelPhotosDto> Photos { get; set; }


        public string CityName { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int Adults { get; set; }
        

    }
}
