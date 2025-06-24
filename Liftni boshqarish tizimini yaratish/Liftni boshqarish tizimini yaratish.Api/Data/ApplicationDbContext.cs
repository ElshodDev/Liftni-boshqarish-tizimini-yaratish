using Liftni_boshqarish_tizimini_yaratish.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Liftni_boshqarish_tizimini_yaratish.Api.Data
{
    public class ApplicationDbContext:DbContext
    {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<ElevatorStatus> ElevatorStatuses { get; set; }
        public DbSet<FloorRequest> FloorRequests { get; set; }
        public DbSet<TimerSession> TimerSessions { get; set; }
         


    }
}
