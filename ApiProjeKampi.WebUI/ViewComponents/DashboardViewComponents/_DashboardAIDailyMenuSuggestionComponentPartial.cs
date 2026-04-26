using System.Net.Http.Headers;
using System.Text;
using ApiProjeKampi.WebUI.DTOS.AISuggestionDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardAIDailyMenuSuggestionComponentPartial : ViewComponent
    {
        private string OPENAI_API_KEY = Environment.GetEnvironmentVariable("OpenAIApiKey");

        private readonly IHttpClientFactory _httpClientFactory;
        public _DashboardAIDailyMenuSuggestionComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. ADIM: Listeyi her zaman boş olarak başlat (View tarafında hata almamak için)
            List<MenuSuggestionDto> menus = new List<MenuSuggestionDto>();

            try
            {
                // 2. ADIM: API Key kontrolü (Environment gelmiyorsa elle yazmayı deneyebilirsin)
                if (string.IsNullOrEmpty(OPENAI_API_KEY))
                {
                    OPENAI_API_KEY = "sk-proj-BURAYA_ANAHTARI_ELLE_YAZIP_DENE";
                }

                var openAiClient = _httpClientFactory.CreateClient();
                openAiClient.BaseAddress = new Uri("https://api.openai.com/");
                openAiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OPENAI_API_KEY);

                string prompt = @"
4 farklı dünya mutfağından tamamen rastgele günlük menü oluştur. Burada ülke isimleri aşağıda verilecektir.

ÖNEMLİ KURALLAR:
- Mutlaka aşağıda verdiğim 4 FARKLI ülke mutfağı seç.
- Daha önce seçtiğin mutfakları tekrar etme (iç mantığında çeşitlilik üret).
- Seçim yapılacak ülkeler: Türkiye, Fransa, Almanya, İtalya, İspanya, Portekiz, Bulgaristan, Gürcistan, Yunanistan, İran, Çin.
- Ülkeleri HER SEFERİNDE FARKLI seç.
- Tüm içerik TÜRKÇE olacak.
- Ülke adını Türkçe yaz (ör: “İtalya Mutfağı”).
- ISO Country Code zorunlu (ör: IT, TR, BG, GE, GR vb.)
- Örnek vermiyorum, tamamen özgün üret.
- Cevap sadece geçerli JSON olsun.

JSON formatı:
[
  {
    ""Cuisine"": ""X Mutfağı"",
    ""CountryCode"": ""XX"",
    ""MenuTitle"": ""Günlük Menü"",
    ""Items"": [
      { ""Name"": ""Yemek 1"", ""Description"": ""Açıklama"", ""Price"": 100 },
      { ""Name"": ""Yemek 2"", ""Description"": ""Açıklama"", ""Price"": 120 },
      { ""Name"": ""Yemek 3"", ""Description"": ""Açıklama"", ""Price"": 90 },
      { ""Name"": ""Yemek 4"", ""Description"": ""Açıklama"", ""Price"": 70 }
    ]
  }
]
";

                var body = new
                {
                    // 3. ADIM: Model ismini düzeltiyoruz (gpt-4o-mini en stabil olanıdır)
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                new { role = "system", content = "Sadece JSON listesi dön." },
                new { role = "user", content = prompt }
            }
                };

                var response = await openAiClient.PostAsync("v1/chat/completions",
                    new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

                var responseJson = await response.Content.ReadAsStringAsync();

                // 4. ADIM: Sadece başarılıysa içeriği oku
                if (response.IsSuccessStatusCode)
                {
                    dynamic obj = JsonConvert.DeserializeObject(responseJson);

                    // Buradaki ?. ve Count kontrolü "RuntimeBinderException" hatasını engeller
                    if (obj?.choices != null && obj.choices.Count > 0)
                    {
                        string aiContent = obj.choices[0].message.content.ToString();
                        menus = JsonConvert.DeserializeObject<List<MenuSuggestionDto>>(aiContent) ?? new List<MenuSuggestionDto>();
                    }
                }
                else
                {
                    // Hata durumunda boş liste döner, Dashboard çökmez
                    Console.WriteLine("API Hatası: " + responseJson);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata: " + ex.Message);
            }

            return View(menus);
        }

    }
}