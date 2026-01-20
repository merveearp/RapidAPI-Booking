# 🌍 RapidAPI Booking & Smart Travel Platform

Bu proje, Booking.com benzeri bir seyahat ve bilgi platformu olarak geliştirilmiştir.  
Otel arama, şehir bazlı dinamik içerikler, finansal veriler, güncel haberler ve yapay zekâ destekli servisleri tek bir sistemde birleştiren kapsamlı bir web uygulamasıdır.  
Proje tamamlanmıştır ve portföy amaçlı hazırlanmıştır.

---

## 🚀 Kullanılan Teknolojiler

- ASP.NET Core MVC
- C#
- DTO Pattern
- Service – Controller mimarisi
- HttpClient
- RapidAPI
- OpenAI API
- Claude (Anthropic API)
- Razor View
- ViewComponent
- Bootstrap, CSS, JavaScript
- JSON Parsing

---

## 🧩 Mimari & Yapısal Yaklaşım

- DTO (Data Transfer Object) yapısı kullanılarak API response’ları ayrıştırılmıştır
- Her dış servis için ayrı Service katmanı oluşturulmuştur
- Controller katmanı yalnızca akış ve yönlendirme görevini üstlenmektedir
- İş mantığı servisler içerisinde yönetilmektedir
- Tüm modüller ViewComponent yapısına ayrılmıştır
- Her API servisi bağımsız ve yeniden kullanılabilir bileşenler olarak tasarlanmıştır
- UI tarafında modüler, okunabilir ve sürdürülebilir bir yapı sağlanmıştır

---

## 🏨 Otel & Konaklama Servisleri (Booking API)

- Lokasyona göre otel arama
- Check-in / Check-out tarihine göre arama
- Otel listeleme
- Otel detay sayfası
- Otel fotoğrafları
- Otel açıklamaları (description)
- Otel puanı ve değerlendirme skorları
- Puan kırılımları (score breakdown)
- Kullanıcı yorumları ve yorum sayıları
- Otel türü ve konaklama bilgileri
- Para birimi bazlı fiyat bilgileri

---

## 🌦️ Hava Durumu Servisi

- Şehir adına göre anlık hava durumu
- Otel araması yapılan şehir ile senkron çalışma
- Varsayılan şehir desteği (İstanbul)

---

## 🧠 Yapay Zekâ Entegrasyonları

### 🗺️ Gezi & Rota Önerisi (OpenAI)

- Şehir adına göre gezilecek yer önerileri
- Kültürel ve turistik rota oluşturma
- Otel araması yapılan şehir ile dinamik çalışma

### 🍽️ Günün Yemeği (Claude + TheMealDB)

- Günlük kültürel yemek önerisi
- Yemek teması ve açıklaması
- Claude (Anthropic) destekli içerik üretimi

---

## 📰 Güncel Haber Servisi

- Türkiye gündemi
- Güncel ekonomi ve finans haberleri
- Başlık ve özet içerik gösterimi

---

## 💰 Finansal Veriler

### Döviz Kurları
- TRY → USD
- TRY → EUR
- TRY → GBP

### Altın Fiyatları
- Güncel alış / satış fiyatları
- Yükseliş – düşüş yön göstergeleri

### Kripto Para
- Bitcoin (BTC) güncel fiyat bilgisi

---

## 🎬 Film Servisi (IMDb)

- IMDb tabanlı film listesi
- Film afişleri
- Film puanı
- Film yılı
- Film açıklamaları
- IMDb linkine yönlendirme
- Slider (yatay kaydırmalı) film gösterimi
- Sağ / sol oklarla kontrollü geçiş

---

## 🔄 Akıllı Sistem Akışı

- Otel araması yapıldığında şehir adına göre:
  - Otel listeleri
  - Hava durumu
  - Yapay zekâ rota önerisi
- API limitleri dolduğunda fallback (default data) mekanizması devreye girer
- Tüm API response’ları DTO yapısı ile yönetilmektedir

---

## 👩‍💻 Geliştirici
GitHub: https://github.com/merveearp
**Merve Arpacıoğlu Türk**  
Junior Backend Developer  
ASP.NET Core | API Integration | AI Supported Projects 

Projeden alıntı görseller :
![uı-1](https://github.com/user-attachments/assets/862ef3bd-ca81-44b9-90e5-db18fa7eaf50)
![uı-2](https://github.com/user-attachments/assets/ffcd95ac-1ba5-474a-beae-760b8fe538fd)
![uı-3](https://github.com/user-attachments/assets/3401023e-1696-402e-a360-4446a673eee7)
![uı-4](https://github.com/user-attachments/assets/95758e7d-8dc3-4dc8-965a-e78bdfc91647)
![uı-1 1](https://github.com/user-attachments/assets/d131a8f3-a455-42c4-9454-ef2028740d22)
![uı-2 2](https://github.com/user-attachments/assets/f7593671-69d9-4ed5-b36e-152cefc30581)
![uı-2 3](https://github.com/user-attachments/assets/d302b1f5-f896-4096-bb5b-46ec810ca89f)
![uı-3 1](https://github.com/user-attachments/assets/06426a46-3ac5-4bed-9a25-9e7cc174796b)
![uı-4 1](https://github.com/user-attachments/assets/f577935b-7b07-4a1b-86b3-bd999ce62461)
![uı-5](https://github.com/user-attachments/assets/42797190-8191-405d-bf0f-b1175faca1aa)
![uı-6](https://github.com/user-attachments/assets/90958b89-9d03-42e1-b85a-b8c81ec4f101)
![uı-8](https://github.com/user-attachments/assets/86247380-2f6a-4745-b925-02e7b3ec1558)
![uı-9](https://github.com/user-attachments/assets/43d57c55-b0a2-4922-8a39-cf703cc792e7)
![uı-10](https://github.com/user-attachments/assets/a7cfbef8-0da8-40b1-bce2-e64311d4e537)

