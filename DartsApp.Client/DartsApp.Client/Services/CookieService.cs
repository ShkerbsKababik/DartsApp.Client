using Microsoft.JSInterop;

namespace DartsApp.Client.Services
{
    public interface ICookieService
    {
        public Task SetCookieAsync(string name, string value);
        public Task<string> GetCookieAsync(string name);
        public Task DeleteCookieAsync(string name);
    }
    public class CookieService : ICookieService
    {
        private readonly IJSRuntime _jsRuntime;

        public CookieService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetCookieAsync(string name, string value)
        {
            await _jsRuntime.InvokeVoidAsync("setCookie", name, value);
        }

        public async Task<string> GetCookieAsync(string name)
        {
            return await _jsRuntime.InvokeAsync<string>("getCookie", name);
        }

        public async Task DeleteCookieAsync(string name)
        {
            await _jsRuntime.InvokeVoidAsync("deleteCookie", name);
        }
    }
}
