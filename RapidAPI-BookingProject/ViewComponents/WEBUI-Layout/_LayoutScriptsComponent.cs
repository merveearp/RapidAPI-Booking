using Microsoft.AspNetCore.Mvc;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Layout
{
    public class _LayoutScriptsComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(); 
        }
    }
}
