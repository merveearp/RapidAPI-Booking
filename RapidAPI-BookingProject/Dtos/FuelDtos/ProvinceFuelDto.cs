namespace RapidAPI_BookingProject.Dtos.FuelDtos
{
    public class ProvinceFuelDto
    {
        public string provinceCode { get; set; }
        public string provinceName { get; set; }
        public List<FuelPriceDto> prices { get; set; }
    }
}