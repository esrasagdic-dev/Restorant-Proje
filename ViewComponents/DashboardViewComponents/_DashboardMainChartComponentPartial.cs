using ApiProjeKampi.WebUI.DTOS.ReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardMainChartComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardMainChartComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7036/"); // Port numarasının doğruluğunu son kez kontrol et

            try
            {
                // BURAYI GÜNCELLEDİK: API'nin beklediği tam adresi yazdık
                var response = await client.GetAsync("api/Reservations/GetReservationStats");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<ReservationChartDto>>(json);
                    return View(data ?? new List<ReservationChartDto>());
                }
                else
                {
                    // Eğer 404 veya 500 hatası alırsan terminale/debug penceresine yazar
                    System.Diagnostics.Debug.WriteLine("API Hatası: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Bağlantı Hatası: " + ex.Message);
            }

            return View(new List<ReservationChartDto>());
        }


    }
}
