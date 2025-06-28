using Liftni_boshqarish_tizimini_yaratish.Api.Data;
using Liftni_boshqarish_tizimini_yaratish.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Liftni_boshqarish_tizimini_yaratish.Api.Controllers.Services
{
    public class ElevatorService
    {
        private readonly ApplicationDbContext _context;

        public ElevatorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ElevatorStatus GetCurrentStatus()
        {
            return _context.ElevatorStatuses.FirstOrDefault() ?? new ElevatorStatus { CurrentFloor = 1 };
        }
        public List<FloorRequest> GetRequests()
        {
            return _context.FloorRequests
                .OrderBy(fr => fr.RequestedAt)
                .ToList();
        }

        public async Task<string> RequestElevatorAsync(int requestedFloor)
        {
            var elevator = GetCurrentStatus();

            if (requestedFloor < 1 || requestedFloor > 10)
                return "Qavat 1 dan 10 gacha bo'lishi kerak.";

            var request = new FloorRequest
            {
                RequestedFloor = requestedFloor,
                RequestedAt = DateTime.Now,
                IsProcessed = false
            };

            _context.FloorRequests.Add(request);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Xato: " + ex.InnerException?.Message);
            }
               _ = Task.Run(ProcessNextRequestAsync);

            return $"Lift {requestedFloor}-qavatga chaqirildi.";
        }
        public async Task ProcessNextRequestAsync()
        {
            var elevator = GetCurrentStatus();
            if (elevator.IsBusy) return;

            // Keyingi qayta ishlanmagan so‘rovni olamiz
            var nextRequest = _context.FloorRequests
                .Where(fr => !fr.IsProcessed)
                .OrderBy(fr => fr.RequestedAt)
                .FirstOrDefault();

            if (nextRequest != null)
            {
                elevator.IsBusy = true;
                elevator.Direction = nextRequest.RequestedFloor > elevator.CurrentFloor ? "up" : "down";
                await _context.SaveChangesAsync();

                // Harakatni simulyatsiya qilish uchun sun'iy kechikish (masalan: 3s delay)
                await Task.Delay(3000);

                elevator.CurrentFloor = nextRequest.RequestedFloor;
                elevator.IsBusy = false;
                elevator.Direction = "idle";
                nextRequest.IsProcessed = true;

                await _context.SaveChangesAsync();
            }
        }


        // CRUD metodlar:
        public async Task<FloorRequest?> GetRequestByIdAsync(int id)
        {
            return await _context.FloorRequests.FindAsync(id);
        }

        public async Task<FloorRequest> CreateRequestAsync(FloorRequest request)
        {
            _context.FloorRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<bool> UpdateRequestAsync(int id, FloorRequest updated)
        {
            var existing = await _context.FloorRequests.FindAsync(id);
            if (existing == null) return false;

            existing.RequestedFloor = updated.RequestedFloor;
            existing.RequestedAt = updated.RequestedAt;
            existing.IsProcessed = updated.IsProcessed;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRequestAsync(int id)
        {
            var existing = await _context.FloorRequests.FindAsync(id);
            if (existing == null) return false;

            _context.FloorRequests.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
