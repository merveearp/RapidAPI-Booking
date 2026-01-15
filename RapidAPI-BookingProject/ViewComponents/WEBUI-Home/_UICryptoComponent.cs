using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ExternalServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UICryptoComponent(IExternalService _externalService) :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var crypto = await _externalService.GetCryptoAsync();
            return View(crypto);
        }
    }
}
