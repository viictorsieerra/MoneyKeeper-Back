using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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


builder.Services.AddScoped<IMetaAhorroRepository, MetaAhorroRepository>(
    provider => new MetaAhorroRepository(connectionString)
);

builder.Services.AddScoped<IMetaAhorroService, MetaAhorroService>();

builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>(
    provider => new TransaccionRepository(connectionString)
);

builder.Services.AddScoped<ITransaccionService, TransaccionService>();

builder.Services.AddScoped<IReciboRepository, ReciboRepository>(
    provider => new ReciboRepository(connectionString)
);

builder.Services.AddScoped<IReciboService, ReciboService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(
    provider => new UsuarioRepository(connectionString)
);

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>(
    provider => new CategoriaRepository(connectionString)
);

builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddScoped<IJwtService, JwtService>();

// Añadir JWT

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            ValidAudience = builder.Configuration["Jwt:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
        };
    });




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//cors

app.UseCors(configurePolicy: policy =>
{
    // policy.WithOrigins("*","https://localhost","http://localhost");
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

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
