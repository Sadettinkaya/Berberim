# BERBERİM

Bu proje, ASP.NET Core MVC teknolojisi kullanılarak geliştirilmiş bir Berber sitesidir. Amaç, berber salonlarının hizmetlerini, çalışanlarını ve randevu süreçlerini kolaylıkla yönetebileceği bir sistem sunmaktır.  
Sistem, kullanıcıların uygun çalışanların uygun saatlerinde işlem bazlı randevu oluşturmasına olanak tanır. Ayrıca, kullanıcıların fotoğraf yükleyerek yapay zeka destekli yazı ve görsel olacak şekilde saç modeli önerileri almasını sağlayan bir entegrasyon da bulunmaktadır.  
PostgreSQL ve Entity Framework Core ile veritabanı yönetimi sağlanırken, kullanıcı dostu bir arayüz sunulması için Bootstrap kullanılmıştır. Uygulamada kullanıcı ve admin panelleri, kimlik doğrulama ve yetkilendirme mekanizmaları ile desteklenmiştir.

---

## Temel Özellikler

- **Personel Yönetimi:** CRUD işlemleri (Ekleme, Güncelleme, Silme, Listeleme).
- **Hizmet Yönetimi:** Hizmetlerin tanımlanması ve düzenlenmesi.
- **Randevu Yönetimi:**
  - Kullanıcıların uygun çalışanlarla randevu oluşturabilmesi.
  - Admin panelinde alınan randevuların onaylanması veya iptali.
- **Uzmanlık ve Müsaitlik Takibi:** Çalışanların uzmanlık alanları ve müsaitlik durumlarının takibi.
- **Kazanç Yönetimi:** Personellerin günlük kazançlarının hesaplanması ve görüntülenmesi.
- **Yapay Zeka Entegrasyonu:**
  - Fotoğraf tabanlı öneri: Kullanıcıların yüklediği fotoğraflar üzerinden saç modeli önerileri.
  - Yazı tabanlı öneri: Kullanıcılara metin bazlı saç stili önerileri.
- **Kimlik Doğrulama:** ASP.NET Core Identity ile üye olma, giriş yapma.
- **Kullanıcı Dostu Arayüz:** Bootstrap tabanlı modern ve dinamik tasarım.
- **REST API:** Personel işlemleri için API entegrasyonu ve Postman testleri.

---

## Uygulama Yapısı

### **Entities Katmanı**
- Veritabanı tabloları ve sütunlar tanımlandı.
- Tablolar arasındaki ilişkiler oluşturuldu.

### **DbContext Katmanı**
- PostgreSQL veritabanı bağlantısı kuruldu.
- Tablo sorgulamaları için `DbSet` nesneleri tanımlandı.

### **Controller'lar**
- **RandevuController:**
  - `YeniRandevu (GET)`: Kullanıcıların yeni randevu oluşturması için form görüntüler.
  - `YeniRandevu (POST)`: Randevu çakışmalarını kontrol eder ve geçerli randevuları kaydeder.
- **PhotoRecommendationController:** Fotoğraf tabanlı yapay zeka önerileri.
- **HomeController:** Ana sayfa ve statik sayfaların yönetimi.
- **LoginController:** Kullanıcı kimlik doğrulama ve oturum işlemleri.
- **PersonelApiController:** REST API işlemleri.

---

## Admin Paneli

Admin paneli, Areas yapısıyla ayrı bir alan olarak tasarlanmıştır. Sadece admin rolündeki kullanıcıların erişebildiği panelde şu işlemler yapılabilir:

- **Personel Yönetimi:** Personel ekleme, silme, güncelleme ve listeleme.
- **Randevu Yönetimi:** Randevuların onaylanması veya iptali.
- **Kazanç Yönetimi:** Personellerin günlük kazançlarının hesaplanması.


---

## Kullanılan Teknolojiler

- **Backend:** ASP.NET Core MVC
- **Frontend:** Bootstrap
- **Veritabanı:** PostgreSQL
- **ORM:** Entity Framework Core
- **Kimlik Doğrulama:** ASP.NET Core Identity
- **Yapay Zeka:** Groq Vision API ve Replicate 
- **REST API:** Personel işlemleri için REST API entegrasyonu.

---


---

## Kurulum

1. **Veritabanı Bağlantısı:**  
   `appsettings.json` dosyasında PostgreSQL bağlantı dizenizi güncelleyin.

2. **Gerekli Bağımlılıkları Yükleyin:**  
   Proje kök dizininde aşağıdaki komutu çalıştırarak bağımlılıkları yükleyin:  
   ```bash
   dotnet restore
