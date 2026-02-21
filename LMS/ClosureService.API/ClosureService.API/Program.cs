using ClosureService.API.Middleware;
using ClosureService.Application.DTO;
using ClosureService.Application.Interfaces;
using ClosureService.Application.Mapping;
using ClosureService.Infrastructure.Data;
using ClosureService.Infrastructure.Repository;
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

builder.Services.AddScoped<IForeclosureRepo, ForeclosureRepo>();
builder.Services.AddScoped<IClosureRepo, ClosureRepo>();

builder.Services.AddHttpClient<LoanTableClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["LoanTableAddress"] ?? "http://localhost:5182");
});

builder.Services.AddHttpClient<InterestChargesClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["InterestChargesAddress"] ?? "http://localhost:5145");
});

builder.Services.AddHttpClient<PaymentClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PaymentAddress"] ?? "http://localhost:5202");
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
