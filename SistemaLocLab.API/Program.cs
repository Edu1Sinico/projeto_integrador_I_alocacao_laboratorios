using Microsoft.EntityFrameworkCore;
using SistemaLocLab.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// ======================================
// BANCO DE DADOS
// ======================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));

// ======================================
// SERVICES
// ======================================

builder.Services.AddControllers();

// ======================================
// SWAGGER / OPENAPI
// ======================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// ======================================
// PIPELINE HTTP
// ======================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();