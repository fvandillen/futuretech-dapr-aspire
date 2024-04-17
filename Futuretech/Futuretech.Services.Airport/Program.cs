using Futuretech.Services.Airport.Metrics;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddSingleton<AirportMetrics>();
builder.AddCustomMeters(["Futuretech.Services.Airport"]);

var app = builder.Build();

app.UseServiceDefaults();

app.Run();