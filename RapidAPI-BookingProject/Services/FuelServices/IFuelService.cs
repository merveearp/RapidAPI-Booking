using RapidAPI_BookingProject.Dtos.ExternalDtos;
using RapidAPI_BookingProject.Dtos.FuelDtos;

namespace RapidAPI_BookingProject.Services.FuelServices
{
    public interface IFuelService
    {
        Task<OpetFuelResponseDto> GetFuelPricesAsync();
    }
}
