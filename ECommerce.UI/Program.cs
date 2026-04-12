using ECommerce.UI.UserInterface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<AdministratorUi>();

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