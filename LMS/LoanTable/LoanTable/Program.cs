using LoanTable.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using LoanTable.Application.Mapping;
using LoanTable.Application.Interfaces;
using LoanTable.Infrastructure.Repository;
using LoanTable.Application.DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped<ILoanRepo, LoanAccountService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<OriginationClient>(client =>
{
  client.BaseAddress = new Uri(builder.Configuration["OriginationAddress"] ?? "http://localhost:5156");
});

builder.Services.AddHttpClient<SanctionClient>(client =>
{
  client.BaseAddress = new Uri(builder.Configuration["SanctionAddress"] ?? "http://localhost:5103");
});

builder.Services.AddDbContext<ApplicationDbContext>(
    Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn"))
  );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
