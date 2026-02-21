using AutoMapper;
using CreditService.Application.DTO.Cibils;
using CreditService.Application.Interfaces.Repositories;
using CreditService.Application.Mapping;
using CreditService.Infrastructure.Repositories;
using CreditService.Infrastucture.Data;
using CreditService.Infrastucture.Services;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped<ICreditRepo, CreditRepo>();
builder.Services.AddScoped<CreditSer>();

builder.Services.AddHttpClient<ClientGetCoustomerId>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["UserAddress"] ?? "http://localhost:5131/api/v1/customer/");
    client.Timeout = TimeSpan.FromMinutes(5);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
