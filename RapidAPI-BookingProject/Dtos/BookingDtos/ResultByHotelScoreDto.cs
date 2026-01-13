namespace RapidAPI_BookingProject.Dtos.BookingDtos
{
    public class ResultByHotelScoreDto
    {
        public int hotel_id { get; set; }           
        public Score_Breakdown[] score_breakdown { get; set; }
        public Score_Percentage[] score_percentage { get; set; }
       
        public class Score_Breakdown
        {
            public string average_score { get; set; }
            public Question[] question { get; set; }

        }

        public class Question
        {
            public string localized_question { get; set; }
            public object score { get; set; }
        }

        public class Score_Percentage
        {       
            public string score_word { get; set; }
         
        }

    }
}
