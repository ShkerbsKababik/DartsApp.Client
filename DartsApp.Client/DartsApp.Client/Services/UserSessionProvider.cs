namespace DartsApp.Client.Services
{
    public interface IUserSessionProvider
    {
        public Guid SessionId { get; set; }
        public event EventHandler<Guid> OnSessionIdChanged;
        public Task<Guid> LoginAsync(string login, string password);
        public void LoguotAsync();
    }
    public class OfflineUserSessionProvider : IUserSessionProvider
    {
        private readonly ICookieService _cookieService;

        private Guid _sessionId;
        public Guid SessionId 
        { 
            get => _sessionId;
            set
            { 
                _sessionId = value;
                OnSessionIdChanged.Invoke(this, value);
            }
        }

        public event EventHandler<Guid> OnSessionIdChanged;

        public OfflineUserSessionProvider(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public async Task<Guid> LoginAsync(string login, string password)
        {
            await _cookieService.SetCookieAsync("userSessionId", Guid.NewGuid().ToString());
            SessionId = Guid.Parse(await _cookieService.GetCookieAsync("userSessionId"));
            return SessionId;
        }

        public async void LoguotAsync()
        {
            await _cookieService.DeleteCookieAsync("userSessionId");
            SessionId = Guid.Empty;
        }       
    }
}
