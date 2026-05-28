using Microsoft.EntityFrameworkCore;
using SistemaLocLab.Infrastructure.Context;
using SistemaLocLab.Infrastructure.Repositories.Implementations;
using SistemaLocLab.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ======================================
// BANCO DE DADOS
// ======================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));

// ======================================
// REPOSITORIES
// ======================================

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ILaboratorioRepository, LaboratorioRepository>();

builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();

builder.Services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();

builder.Services.AddScoped<IAlocacaoRepository, AlocacaoRepository>();

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