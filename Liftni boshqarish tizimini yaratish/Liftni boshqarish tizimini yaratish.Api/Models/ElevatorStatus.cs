namespace Liftni_boshqarish_tizimini_yaratish.Api.Models
{
    public class ElevatorStatus
    {
      public int Id { get; set; }
      public int CurrentFloor { get; set; } = 1; 
      public string Direction { get; set; } = "idle"; // 'up', 'down', 'idle'
      public bool IsBusy { get; set; } = false;
    }
}
