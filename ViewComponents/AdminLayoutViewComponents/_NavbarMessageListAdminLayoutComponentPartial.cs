using ApiProjeKampi.WebUI.DTOS.ChefDtos;
using ApiProjeKampi.WebUI.DTOS.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.AdminLayoutNavbarViewComponents
{
    public class _NavbarMessageListAdminLayoutComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _NavbarMessageListAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7036/api/Messages/MessageListByIsReadFalse");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageByIsReadFalseDto>>(jsonData);

                // Eğer API'den liste boş gelirse null yerine boş liste dönmek hayat kurtarır.
                return View(values ?? new List<ResultMessageByIsReadFalseDto>());
            }

            // HATA BURADAYDI: Parantez içini boş bırakmak 'null' gönderir. 
            // Boş bir liste gönderiyoruz ki HTML tarafındaki .Count hata vermesin.
            return View(new List<ResultMessageByIsReadFalseDto>());
        }
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var responseMessage = await client.GetAsync("https://localhost:7036/api/Messages/MessageListByIsReadFalse");
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        //        var values = JsonConvert.DeserializeObject<List<ResultMessageByIsReadFalseDto>>(jsonData);
        //        return View(values);
        //    }
        //    return View();
        //}
    }
}
