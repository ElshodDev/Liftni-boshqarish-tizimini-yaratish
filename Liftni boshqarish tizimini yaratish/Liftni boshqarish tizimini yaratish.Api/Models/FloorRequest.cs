namespace Liftni_boshqarish_tizimini_yaratish.Api.Models
{
    public class FloorRequest
    {
        public int Id { get; set; }
        public int RequestedFloor { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.Now;
        public bool IsProcessed { get; set; } = false;
    }
}
