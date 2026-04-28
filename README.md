# 🍽️ Api Proje Kampı - Restoran Rezervasyon Sistemi

Bu proje, sıfırdan API mantığını kavramayı ve bir tüketici (UI) uygulamasıyla entegrasyonunu amaçlayan bir web uygulamasıdır.

## 📌 Proje Hakkında
Proje, bir restoranın rezervasyon süreçlerini, menü yönetimini ve müşteri geri bildirimlerini dijital ortamda yönetmek için tasarlanmıştır. Temel odak noktası, katmanlı mimari karmaşasına girmeden **API çalışma prensiplerini**, **HTTP metotlarını** ve **verilerin UI tarafında tüketilmesini** öğrenmektir.

## 🚀 Kullanılan Teknolojiler & Araçlar
* **Backend:** ASP.NET Core 6.0 (Kararlı sürüm olması ve Swagger desteği için tercih edilmiştir.)
* **Veritabanı:** MSSQL
* **ORM:** Entity Framework Core (Code First Yaklaşımı)
* **API Test:** Swagger & Postman
* **UI:** Single Page Application (SPA) Teması üzerine entegre edilmiş ASP.NET Core UI

## ✨ Öne Çıkan Özellikler
* **Rezervasyon Sistemi (Book a Table):** Dinamik rezervasyon formu ve yönetimi.
* **Dinamik Menü:** Başlangıçlar, öğle yemeği ve akşam yemeği gibi kategorilere ayrılmış dinamik menü yönetimi.
* **İstatistik Paneli:** Müşteri sayısı, proje sayısı ve çalışan sayısı gibi verilerin dinamik olarak gösterilmesi.
* **Müşteri Yorumları (Testimonials):** Kullanıcı deneyimlerinin sergilendiği alan.
* **Video Entegrasyonu:** Popup üzerinde çalışan gömülü (embedded) video oynatıcı.

## 🛠️ Teknik Detaylar
* **API Attributes:** Route, HttpGet, HttpPost, HttpPut, HttpDelete kullanımları.
* **Token Bazlı Güvenlik:** İlerleyen aşamalarda JSON Web Token (JWT) entegrasyonu.
* **Consume İşlemleri:** API üzerinden gelen verilerin UI tarafında işlenmesi ve gösterilmesi.

## 🏗️ Mimari Yapı
Proje iki ana katmandan oluşmaktadır:
1. **API Katmanı:** Veritabanı işlemlerinin ve iş mantığının yürütüldüğü, verilerin JSON olarak sunulduğu kısım.
2. **UI Katmanı:** API'den gelen verileri kullanıcıya görsel bir arayüzle sunan katman.
