using DartsApp.Client;
using DartsApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// add HttpClient
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5213")
    });

// Mud Blazor
builder.Services.AddMudServices();

builder.Services.AddScoped<IUserSessionProvider, OfflineUserSessionProvider>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<IGameService, GameService>();

await builder.Build().RunAsync();

// Создание пользователей
// Возможность выбора оффлайн и не офлайн игры
// Удаление прошлых значений
// Формульный подсчет
// Смена библиотеки UI
// Форма для подключения к игре по Id
// Многопользовательский режим -> возможность открыть игру с нескольких пк и вносить значения самостоятельно
// Возможность настройки игры перед стартом (отключение случайного порядка игроков)
// Завершение игры
// Подсчет предварительного счета (считать сколько осталось до применения счета)
// Переход к следущему значению хода по стрелочкам или по ентер
// Текст-посказака с биндами ниже формы внесени результатов