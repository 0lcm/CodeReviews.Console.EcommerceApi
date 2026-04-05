using System.Text.Json.Serialization;
using ECommerce.API.Data;
using ECommerce.API.Interfaces;
using ECommerce.API.Repositories;
using ECommerce.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlite(DbConfig.GetConnectionString()));

builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApiDbContext>();

    var dbDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ECommerce");
    Directory.CreateDirectory(dbDirectory);
    
    var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
    var isFirstRun = pendingMigrations.Any();
    
    await db.Database.MigrateAsync();

    if (isFirstRun) await DbSeeder.SeedItemsAsync(db);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler(error => error.Run(async context =>
{
    context.Response.StatusCode = 500;
    await context.Response.WriteAsync("An unexpected exception occurred during runtime.");
}));

app.Run();