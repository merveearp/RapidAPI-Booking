namespace RapidAPI_BookingProject.Dtos
{
    public class ResultHotelListDto
    {      
        public int id { get; set; }
        public string name { get; set; }
        public string photoMainUrl { get; set; }
        public string countryCode { get; set; }
        public string currency { get; set; }
        public string checkoutDate { get; set; }
        public string checkinDate { get; set; }
        public float reviewScore { get; set; }
        public int reviewCount { get; set; }
        public string wishlistName { get; set; }
        public string propertyType { get; set; }


        }

    }

