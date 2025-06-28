    using Liftni_boshqarish_tizimini_yaratish.Api.Data;
    using Liftni_boshqarish_tizimini_yaratish.Api.Models;

    namespace Liftni_boshqarish_tizimini_yaratish.Api.Controllers.Services
    {
        public class TimerService
        {
            private readonly ApplicationDbContext _context;
            private TimerSession? _currentSession;
            private bool _isRunning = false;
            private DateTime _startTime;
            private TimeSpan _duration;

            public TimerService(ApplicationDbContext context)
            {
                _context = context;
            }
            public string StartTimer(int seconds)
            {
                if (_isRunning)
                    return "Taymer allaqachon ishga tushgan";

                _startTime = DateTime.Now;
                _duration = TimeSpan.FromSeconds(seconds);
                _isRunning = true;

                return $"Taymer boshlandi: {_startTime}, tugash vaqti: {_startTime + _duration}";
            }
            public string StopTimer()
            {
                if (!_isRunning)
                    return "Taymer ishlamayapti";

                var endTime = DateTime.Now;
                _currentSession = new TimerSession
                {
                    StartedAt = _startTime,
                    EndedAt = endTime,
                };
                _context.TimerSessions.Add(_currentSession);
                _context.SaveChanges();

                _isRunning = false;

                return $"Taymer to‘xtadi. Boshlangan: {_startTime}, tugagan: {endTime}";
            }
            public object GetCurrent()
            {
                if (!_isRunning)
                    return new { Status = "Taymer ishlamayapti" };

                var remaining = (_startTime + _duration) - DateTime.Now;

                return new
                {
                    Status = "Ishlayapti",
                    StartedAt = _startTime,
                    EndsAt = _startTime + _duration,
                    RemainingSeconds = Math.Max(0, (int)remaining.TotalSeconds)
                };
            }

            public List<TimerSession> GetHistory()
            {
                return _context.TimerSessions
                    .OrderByDescending(t => t.StartedAt)
                    .ToList();
            }
            public TimerSession? GetById(int id)
            {
                return _context.TimerSessions.FirstOrDefault(t => t.Id == id);
            }
            public string Update(int id, TimerSession updated)
            {
                var session = _context.TimerSessions.FirstOrDefault(t => t.Id == id);
                if (session == null)
                    return "Session topilmadi";

                session.StartedAt = updated.StartedAt;
                session.EndedAt = updated.EndedAt;
                _context.SaveChanges();
                return "Session yangilandi";
            }
            public string Delete(int id)
            {
                var session = _context.TimerSessions.FirstOrDefault(t => t.Id == id);
                if (session == null)
                    return "Session topilmadi";

                _context.TimerSessions.Remove(session);
                _context.SaveChanges();
                return "Session o‘chirildi";
            }
        }
    }
