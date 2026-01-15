namespace RapidAPI_BookingProject.Dtos.ExternalDtos
{
    public class ResultInflationDto
    {

        public class Rootobject
        {
           
        }

        public class Class1
        {
            public string country { get; set; }
            public string country_code { get; set; }
            public string type { get; set; }
            public string period { get; set; }
            public float monthly_rate_pct { get; set; }
            public float yearly_rate_pct { get; set; }
        }

    }
}
