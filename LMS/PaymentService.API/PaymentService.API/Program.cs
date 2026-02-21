using Microsoft.EntityFrameworkCore;
using PaymentService.Application.DTO;
using PaymentService.Application.Interfaces;
using PaymentService.Application.Mapping;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("dbconn")
    ));

builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();

builder.Services.AddHttpClient<LoanTableClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["LoanTableAddress"] ?? "http://localhost:5182");
});

builder.Services.AddHttpClient<InterestChargesClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["InterestChargesAddress"] ?? "http://localhost:5145");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
