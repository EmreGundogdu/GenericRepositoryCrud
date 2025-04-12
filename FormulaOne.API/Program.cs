using FormulaOne.API.Services;
using FormulaOne.API.Services.Interfaces;
using FormulaOne.Data.Data;
using FormulaOne.Data.Repositories;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Services.Email;
using FormulaOne.Services.Email.Interfaces;
using Hangfire;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddDbContext<AppDbContext>(opt =>opt.UseSqlite(connectionString: builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IMerchService, MerchService>();
builder.Services.AddScoped<IDriverNotificationPublisherService, DriverNotificationPublisherService>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("myuser");
            h.Password("mypass");
        });
    });
});

builder.Services.AddHangfire(cfg =>
{
    cfg.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
    cfg.UseSimpleAssemblyNameTypeSerializer();
    cfg.UseRecommendedSerializerSettings();
    cfg.UseSQLiteStorage(builder.Configuration.GetConnectionString("HangfireConnection"));
});

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.UseRouting();

app.MapControllers();

app.UseHangfireDashboard();
app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    DashboardTitle = "Hangfire FormulaOne",
    Authorization =  new []
    {
        new HangfireCustomBasicAuthenticationFilter()
        {
            Pass = "pass",
            User = "user",
        }
    }
});

RecurringJob.AddOrUpdate(()=>Console.WriteLine("Hello World!"), Cron.Minutely);

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
