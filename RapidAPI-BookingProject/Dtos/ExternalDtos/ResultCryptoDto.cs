namespace RapidAPI_BookingProject.Dtos.ExternalDtos
{
    public class ResultCryptoDto
    {

        public string from { get; set; }
        public To to { get; set; }

        public class To
        {
            
            public float BTC { get; set; }
            public float ETH { get; set; }
            public float BNB { get; set; }
        }
  

    }
}
