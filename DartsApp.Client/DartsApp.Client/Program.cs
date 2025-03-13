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

// �������� �������������
// ����������� ������ ������� � �� ������ ����
// �������� ������� ��������
// ���������� �������
// ����� ���������� UI
// ����� ��� ����������� � ���� �� Id
// ��������������������� ����� -> ����������� ������� ���� � ���������� �� � ������� �������� ��������������
// ����������� ��������� ���� ����� ������� (���������� ���������� ������� �������)
// ���������� ����
// ������� ���������������� ����� (������� ������� �������� �� ���������� �����)
// ������� � ��������� �������� ���� �� ���������� ��� �� �����
// �����-��������� � ������� ���� ����� ������� �����������