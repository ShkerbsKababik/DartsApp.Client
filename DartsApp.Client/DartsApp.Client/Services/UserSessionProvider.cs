namespace DartsApp.Client.Services
{
    public interface IUserSessionProvider
    {
        public Guid SessionId { get; set; }
        public Guid Login(string login, string password);
        public void Logout(Guid sessionId);
    }
    public class OfflineUserSessionProvider : IUserSessionProvider
    {
        private readonly ICookieService _cookieService;

        public Guid SessionId { get; set; }

        public OfflineUserSessionProvider(ICookieService cookieService)
        {
            _cookieService = cookieService;

            //string cookie = _cookieService.GetCookieAsync("userSessionId").Result;
            //SessionId = Guid.Parse(cookie);
        }

        public Guid Login(string login, string password)
        {
            Guid guid = Guid.NewGuid();

            _cookieService.SetCookieAsync("userSessionId", guid.ToString());
            SessionId = guid;
            return guid;
        }

        public void Logout(Guid sessionId)
        {
            _cookieService.DeleteCookieAsync("userSessionId");
            SessionId = Guid.Empty;
        }
    }
}
