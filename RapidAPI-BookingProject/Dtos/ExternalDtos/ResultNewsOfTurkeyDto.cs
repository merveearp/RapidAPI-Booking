namespace RapidAPI_BookingProject.Dtos.ExternalDtos
{
    public class ResultNewsOfTurkeyDto
    {

        public Datum[] data { get; set; }

        public class Datum
        {
            public string article_id { get; set; }
            public string title { get; set; }
            public string link { get; set; }
            public string snippet { get; set; }
            public string photo_url { get; set; }
            public object[] authors { get; set; }
            public string source_url { get; set; }
            public string source_name { get; set; }

        }

    }
}
