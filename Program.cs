using Repositories;
using Services;


var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("MoneyKeeper");

// Add services to the container.

//Añadido de dependecias

builder.Services.AddScoped<ICuentaRepository, CuentaRepository>(
    provider => new CuentaRepository(connectionString)
);

builder.Services.AddScoped<ICuentaService, CuentaService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
