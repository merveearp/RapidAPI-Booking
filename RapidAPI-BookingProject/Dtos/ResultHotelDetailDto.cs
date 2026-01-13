namespace RapidAPI_BookingProject.Dtos
{
    public class ResultHotelDetailDto
    {
        public int hotel_id { get; set; }
        public string hotel_name { get; set; }
        public string hotel_name_trans { get; set; }
        public string url { get; set; }
        public string accommodation_type_name { get; set; }
        public string hotel_address_line { get; set; }
        public string country_trans { get; set; }
        public string countrycode { get; set; }
        public string district { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string address_trans { get; set; }
        public string timezone { get; set; }

        public Facilities_Block facilities_block { get; set; }
        public class Facilities_Block
        {
            public string name { get; set; }
            public Facility[] facilities { get; set; }
            public string type { get; set; }
        }

        public class Facility
        {
            public string icon { get; set; }
            public string name { get; set; }
        }

        public class Icon_List
        {
            public int size { get; set; }
            public string icon { get; set; }
        }


    }

}
