using RapidAPI_BookingProject.Dtos;

namespace RapidAPI_BookingProject.Models
{

        public class HotelDetailViewModel
        {

            public ResultHotelDetailDto Detail { get; set; }
            public List<ResultByHotelPhotosDto> Photos { get; set; }


            public string CheckIn { get; set; }
            public string CheckOut { get; set; }
        }

    }

