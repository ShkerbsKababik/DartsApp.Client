using DartsApp.Client;
using DartsApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IUserSessionProvider, OfflineUserSessionProvider>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<IGameSessionHandler, GameSessionHandler>();

await builder.Build().RunAsync();
