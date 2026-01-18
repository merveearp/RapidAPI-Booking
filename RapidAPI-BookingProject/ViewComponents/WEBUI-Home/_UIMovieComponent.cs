using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ExternalServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIMovieComponent(IExternalService _externalService ):ViewComponent
    {
       public async Task<IViewComponentResult> InvokeAsync()
        {
            var movies = await _externalService.GetMovieAsync();
            return View(movies);
        }
    }
}
