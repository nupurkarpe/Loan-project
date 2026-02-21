using InterestAndChargesServices.API.Middleware;
using InterestAndChargesServices.Application.DTO;
using InterestAndChargesServices.Application.Interfaces;
using InterestAndChargesServices.Application.Mapping;
using InterestAndChargesServices.Infrastructure.Data;
using InterestAndChargesServices.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddScoped<IInterestRepo, InterestRepo>();
builder.Services.AddScoped<IPenaltyRepo, PenaltyRepo>();

builder.Services.AddHttpClient<LoanTableClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["LoanTableAddress"] ?? "http://localhost:5182");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        var user = httpContext.User.Identity?.IsAuthenticated == true
            ? httpContext.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "Unknown"
            : "Anonymous";
        diagnosticContext.Set("User", user);
        var routeData = httpContext.GetRouteData();
        if (routeData != null)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];
            if (controller != null) diagnosticContext.Set("Controller", controller);
            if (action != null) diagnosticContext.Set("Action", action);
        }
    };
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
