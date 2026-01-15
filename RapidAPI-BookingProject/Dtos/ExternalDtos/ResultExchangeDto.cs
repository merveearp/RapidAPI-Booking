

namespace RapidAPI_BookingProject.Dtos.ExternalDtos
{
    public class ResultExchangeDto
    {
        public string base_code { get; set; }
        public Rates rates { get; set; }
        public class Rates
        {
            public int TRY { get; set; }
            public float USD { get; set; }
            public float EUR { get; set; }
            public float GBP { get; set; }


        }
    }
    }
