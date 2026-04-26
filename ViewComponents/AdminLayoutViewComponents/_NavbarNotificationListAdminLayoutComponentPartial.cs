using ApiProjeKampi.WebUI.DTOS.MessageDtos;
using ApiProjeKampi.WebUI.DTOS.NotificationDtos;
using ApiProjeKampi.WebUI.ViewComponents.AdminLayoutViewComponents;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.AdminLayoutNavbarViewComponents
{
    public class _NavbarNotificationAdminLayoutComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _NavbarNotificationAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7036/api/Notifications");

            if (responseMessage.IsSuccessStatusCode)
            {
        
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultNotificationDto>>(jsonData);

                // ÖNLEM 1: Liste null gelirse boş bir liste gönder ki .Count hata vermesin
                return View(values ?? new List<ResultNotificationDto>());
            }

            // ÖNLEM 2: API kapalıysa veya hata verirse 'null' yerine boş liste dön
            return View(new List<ResultNotificationDto>());
        }
    }
}
