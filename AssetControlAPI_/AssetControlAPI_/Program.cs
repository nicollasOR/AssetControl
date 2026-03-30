using DotNetEnv;
using AssetControlAPI_.Contexts;
using Microsoft.EntityFrameworkCore;
using AssetControlAPI_.Interface;
using AssetControlAPI_.Repository;
using AssetControlAPI_.Applications.Services;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;
builder.Services.AddDbContext<AssetDBContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBairroRepository, BairroRepository>();
builder.Services.AddScoped<BairroService>();

builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<CidadeService>();

builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<AreaService>();

builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddScoped<LocalizacaoService>();

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
