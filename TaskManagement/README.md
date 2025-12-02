# TaskManagement API

Basit bir **Görev Yönetimi (Task Management) REST API** uygulamasıdır.
ASP.NET Core ile oluşturulmuş, Entity Framework Core kullanarak **LocalDB üzerinde gerçek veritabanı** ile çalışır.

---

## 🚀 Özellikler

* Görev ekleme (Create)
* Görevleri listeleme (Read)
* Görev güncelleme (Update)
* Görev silme (Delete)
* Entity Framework Core ile SQL veritabanı bağlantısı
* Katmanlı yapı:

  * **Models**
  * **Data (DbContext)**
  * **Repositories**
  * **Services**
  * **Controllers**

---

## 🏗 Kullanılan Teknolojiler

* **.NET 8 Web API**
* **Entity Framework Core**
* **SQL Server LocalDB**
* **C#**
* **REST mimarisi**

---

## 📁 Proje Yapısı

```
TaskManagement/
 ├── Controllers/
 │     └── TasksController.cs
 ├── Models/
 │     └── TaskItem.cs
 ├── Data/
 │     └── AppDbContext.cs
 ├── Repositories/
 │     └── TaskRepository.cs
 ├── Services/
 │     └── TaskService.cs
 ├── Migrations/
 ├── appsettings.json
 ├── Program.cs
 └── TaskManagement.csproj
```

---

## 🔌 API Endpointleri

### ✔ GET /api/tasks

Tüm görevleri getirir.

### ✔ GET /api/tasks/{id}

Belirli bir görevi getirir.

### ✔ POST /api/tasks

Yeni görev ekler.

Örnek body:

```json
{
  "title": "Ders çalış",
  "description": "Bilgi sistemleri ödevi",
  "isCompleted": false
}
```

### ✔ PUT /api/tasks

Var olan görevi günceller.

### ✔ DELETE /api/tasks/{id}

Görev siler.

---

## 🗄 Veritabanı

Proje **LocalDB** kullanmaktadır.

`appsettings.json` içindeki bağlantı dizesi:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\\\MSSQLLocalDB;Database=TaskDb;Trusted_Connection=True;"
}
```

Migration işlemleri:

```
Add-Migration InitialCreate
Update-Database
```

---

## ▶ Uygulamayı Çalıştırma

Terminalden:

```
dotnet run
```

Tarayıcı veya Postman üzerinden istek atılabilir:

```
http://localhost:5210/api/tasks
```

---

## 📌 Notlar

* FakeTaskService **artık kaldırılmıştır**, sistem tamamen gerçek veritabanı kullanır.
* Proje öğrenme amaçlı geliştirilmiştir.
* Katmanlı yapıya uygun şekilde genişletilebilir.

---

## 📝 Lisans

Bu proje eğitim amaçlıdır.
