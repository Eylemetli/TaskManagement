# TaskManagement API

Basit ama tam fonksiyonel bir **Görev Yönetimi (Task Management) REST API** uygulamasıdır.  
ASP.NET Core (.NET 8) ile geliştirilmiş, Entity Framework Core kullanarak **SQL Server LocalDB** üzerinde gerçek veritabanı ile çalışır.

---

## 🚀 Özellikler

- Görev ekleme (**Create**)
- Görevleri listeleme (**Read**)
- Görev güncelleme (**Update**)
- Görev silme (**Delete**)
- Entity Framework Core ile SQL veritabanı bağlantısı
- **DTO + Validation** yapısı:
  - `TaskCreateDto`, `TaskUpdateDto`
  - DataAnnotations + FluentValidation ile alan kontrolleri
- **Unit testler**:
  - Service katmanı testleri
  - Controller ve validator testleri (xUnit)
- Swagger UI ile API dokümantasyonu
- React frontend ile kullanılmak üzere **CORS ayarlı** API

---

## 🏗 Kullanılan Teknolojiler

- **.NET 8 Web API (ASP.NET Core)**
- **Entity Framework Core (SQL Server)**  
- **SQL Server LocalDB**
- **FluentValidation**
- **xUnit** (unit testler için)
- **C#**
- **REST mimarisi**

---

## 📁 Proje Yapısı

> Temel klasör yapısı (backend tarafı):


TaskManagement/
 ├── Controllers/
 │     └── TasksController.cs
 ├── Models/
 │     ├── TaskItem.cs
 │     ├── TaskCreateDto.cs
 │     └── TaskUpdateDto.cs
 ├── Validators/
 │     └── TaskCreateDtoValidator.cs
 ├── Data/
 │     └── AppDbContext.cs
 ├── Services/
 │     ├── ITaskService.cs
 │     └── TaskService.cs
 ├── Migrations/
 ├── Program.cs
 ├── appsettings.json
 └── TaskManagement.csproj

Ayrıca solution içinde ayrı bir test projesi bulunur:

TaskManagement.Tests/
 ├── TaskServiceTests.cs
 ├── TasksControllerTests.cs
 └── TaskCreateDtoValidatorTests.cs


🔌 API Endpointleri
Temel endpointler:

✔ GET /api/tasks
Tüm görevleri getirir.

✔ GET /api/tasks/{id}
Belirli bir görevi Id’ye göre getirir.
Bulunamazsa 404 NotFound döner.

✔ POST /api/tasks
Yeni görev ekler.
Örnek istek gövdesi (body):

{
  "title": "Ders çalış",
  "description": "Bilgi sistemleri ödevi",
  "isCompleted": false
}

Validasyon kuralları (özet):

Title:
Zorunlu ([Required])
En az 3 karakter (FluentValidation)

Description:
Zorunlu ([Required])
En az 5 karakter (FluentValidation)

Validasyon hatalarında 400 BadRequest ile anlamlı hata mesajları döner.

✔ PUT /api/tasks
Var olan bir görevi günceller.

Örnek body:

{
  "id": 1,
  "title": "Ders çalış (güncellendi)",
  "description": "Bilgi sistemleri ödevi - güncellendi",
  "isCompleted": true
}
Id bulunamazsa 404 NotFound

Geçersiz veri varsa 400 BadRequest

✔ DELETE /api/tasks/{id}
İlgili Id’ye sahip görevi siler.
Silinirse 200 OK + basit bir mesaj
Kayıt yoksa 404 NotFound

🗄 Veritabanı
Proje LocalDB kullanmaktadır.
appsettings.json içindeki örnek bağlantı dizesi:
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\\\MSSQLLocalDB;Database=TaskManagementDb;Trusted_Connection=True;"
}

Migration işlemleri (Package Manager Console):
Add-Migration InitialCreate
Update-Database

🧪 Unit Testler
Solution içinde TaskManagement.Tests isimli bir test projesi bulunur.
Test edilen başlıca parçalar:

TaskService:
Tüm görevleri listeleme
Görev ekleme / güncelleme / silme senaryoları

TasksController:
Status code’ların ve sonuçların doğru dönmesi

TaskCreateDtoValidator:
Boş / kısa title & description senaryoları
Geçerli model için başarılı validasyon

Testler xUnit ile yazılmıştır ve Visual Studio Test Explorer üzerinden veya komut satırından çalıştırılabilir.

🌐 Frontend Entegrasyonu (CORS)
Bu API, Vite + React ile yazılmış bir frontend tarafından tüketilecek şekilde tasarlanmıştır.

CORS ayarı ile https://localhost:5173 adresinden gelen istekler kabul edilir.

Frontend tarafında:
Görev listeleme, ekleme, silme
Tamamlandı / bekliyor durumu güncelleme
İstatistik paneli ve mini takvim
API üzerinden beslenir.

▶ Uygulamayı Çalıştırma
 Veritabanı
Migration’lar uygulanmamışsa:
Add-Migration InitialCreate
Update-Database

API’yi çalıştırma
Terminalden:
dotnet run
veya Visual Studio üzerinden F5 / Debug.

Swagger UI
Uygulama çalıştığında Swagger arayüzü genelde şu adreslerden birinde açılır:
https://localhost:7256/swagger
veya
https://localhost:7256/swagger/index.html
Buradan endpointleri test edebilir, request/response yapısını görebilirsiniz.

📌 Notlar
FakeTaskService kaldırılmıştır, sistem tamamen gerçek veritabanı ile çalışmaktadır.

Proje, öğrenme amaçlı geliştirilmiş bir backend başlangıç projesidir.

Katmanlı yapıyı koruyacak şekilde yeni özellikler (etiketleme, son tarih, kullanıcı bazlı görevler vb.) kolayca eklenebilir.

## İlgili Frontend

Bu API, şu React frontend tarafından tüketilmektedir:

https://github.com/Eylemetli/taskmanagement-frontend

