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

            return $"Lift {requestedFloor}-qavatga chaqirildi.";
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
