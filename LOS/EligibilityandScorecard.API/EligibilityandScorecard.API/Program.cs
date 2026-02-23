using EligibilityandScorecard.Application.Interfaces.Repositories;
using EligibilityandScorecard.Infrastructure.Data;
using EligibilityandScorecard.Infrastructure.ExternalServices;
using EligibilityandScorecard.Infrastructure.Repositories;
using EligibilityandScorecard.Infrastructure.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")
    )
);

builder.Services.AddHttpClient<CreditApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5034/"); // Credit Service URL
});

builder.Services.AddScoped<IEligibilityRepo, EligibilityRepo>();
builder.Services.AddScoped<EligibilityService>();

builder.Services.AddScoped<IScorecardRepo, ScorecardRepo>();
builder.Services.AddScoped<IScorecardService, ScorecardService>();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("global", opt =>
    {
        opt.PermitLimit = 100;                  // 100 requests
        opt.Window = TimeSpan.FromMinutes(3);   // per 3 minutes
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 20;
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("global");

app.Run();
