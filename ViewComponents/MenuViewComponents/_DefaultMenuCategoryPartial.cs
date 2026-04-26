using ApiProjeKampi.WebUI.DTOS.CategoryDto;
using ApiProjeKampi.WebUI.DTOS.ServiceDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.MenuViewComponents
{
    public class _DefaultMenuCategoryPartial: ViewComponent
    {
        private readonly IHttpClientFactory  _httpClientFactory;

        public _DefaultMenuCategoryPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7036/api/Categories/");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>
                    (jsonData);
                return View(values);
            }
            return View();
        }
    }
}
