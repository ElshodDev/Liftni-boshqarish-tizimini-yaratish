Liftni boshqarish tizimi

Bu loyiha liftni qavatlar orasida boshqarish, so'rovlarni qayta ishlash va taymer funksiyalarini amalga oshiruvchi ASP.NET Core Web API tizimidir.

 Ishga tushirish (Docker bilan)
--------------------------------
1. Talablar:
   - Docker
   - Docker Compose

2. Terminalda quyidagilarni bajaring:
   docker-compose up --build

Bu quyidagilarni ishga tushiradi:
- webapi (ASP.NET Core API)
- db (PostgreSQL 15)

 API endpointlar
-------------------

Lift (Elevator):
- GET /api/elevator/status — Liftning joriy holatini olish
- GET /api/elevator/requests — So‘rovlar ro‘yxati (FIFO)
- POST /api/elevator/move?floor=5 — Liftni qavatga yuborish

CRUD so‘rovlar:
- GET /api/elevator/request/{id} — So‘rovni olish
- POST /api/elevator/request — Yangi so‘rov yaratish
- PUT /api/elevator/request/{id} — So‘rovni yangilash
- DELETE /api/elevator/request/{id} — So‘rovni o‘chirish

Taymer (Timer):
- POST /api/timer/start?seconds=10 — Taymerni ishga tushirish
- POST /api/timer/stop — Taymerni to‘xtatish
- GET /api/timer/current — Hozirgi taymer holatini olish
- GET /api/timer/history — Oldingi sessiyalar ro‘yxati
- GET /api/timer/{id} — Taymer sessiyasini olish
- PUT /api/timer/{id} — Sessiyani yangilash
- DELETE /api/timer/{id} — Sessiyani o‘chirish

 Ma'lumotlar bazasi
-----------------------
Jadval(lar):
- ElevatorStatus — Liftning joriy holati (qavat, yo‘nalish, bandlik)
- FloorRequest — Foydalanuvchi so‘rovlar
- TimerSession — Taymer sessiyalari

 Docker fayllar
------------------
- Dockerfile — Web API uchun
- docker-compose.yml — Web API + PostgreSQL

📂 Loyihaning umumiy tarkibi
------------------------------
- Controllers/ — API controllerlari
- Controllers/Services/ — Biznes mantiq
- Models/ — Ma'lumotlar modellari
- Data/ApplicationDbContext.cs — EF Core konteksti

 Qo‘shimcha
---------------
- Lift FIFO tartibida ishlaydi
- Taymer sessiyalari avtomatik saqlanadi
- API’ni Postman yoki Swagger orqali test qilish mumkin

----------------------------------

Muallif: Elshod Ibodullayev
Loyiha nomi: Liftni boshqarish tizimi
Texnologiyalar: ASP.NET Core, PostgreSQL, Docker, EF Core
