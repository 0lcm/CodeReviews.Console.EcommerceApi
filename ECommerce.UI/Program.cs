using ECommerce.UI.AdministratorUi;
using ECommerce.UI.Configuration;
using ECommerce.UI.Interfaces;
using ECommerce.UI.Interfaces.AdministratorInterfaces;
using ECommerce.UI.Services;
using ECommerce.UI.Services.AdministratorServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<IApiService, ApiService>();

builder.Services.AddTransient<IManageProductsService, ManageProductsService>();
builder.Services.AddTransient<ManageProductsUi>();

builder.Services.AddTransient<AdministratorUi>();

builder.Services.AddHttpClient(ApiSettings.BaseUrl, 
    client => client.BaseAddress = new Uri(ApiSettings.BaseUrl));
builder.Services.AddHostedService<Worker>();

builder.Logging.SetMinimumLevel(LogLevel.Warning);

var app = builder.Build();

await app.RunAsync();

internal class Worker : BackgroundService
{
    private readonly AdministratorUi _adminUi;

    public Worker(AdministratorUi adminUi)
    {
        _adminUi = adminUi;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _adminUi.MainMenu();
    }
}