using Microsoft.AspNetCore.Mvc;
using RapidAPI_BookingProject.Services.ExternalServices;

namespace RapidAPI_BookingProject.ViewComponents.WEBUI_Home
{
    public class _UIArticleComponent(IExternalService _externalService) :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var articles = await _externalService.GetArticlesAsync();

            var limitedArticles = articles
                .Where(x => x.images?.thumbnail != null)
                .Take(12)
                .ToList();

            return View(limitedArticles);
        }

    }
}

