using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //1 Adiciona o suporte aos Controllers
        builder.Services.AddControllers();


        var configuration = builder.Configuration;

        // 2 Configura o Swgger/OpenAPI

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Alocação de Laboratórios WEB.API",
                Version = "v1",
                Description = "API para alocação de laboratórios"
                
        });
        });

        builder.Services.AddDbContext<ApplicationDbContext>(opcao =>
            opcao.UseNpgsql(configuration.GetValue<string>("Settings:CONNECTION_STRING"),o => o.UseRelationalNulls()));


        builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

        var app = builder.Build();

        // 3 Pipeline de processamento das requisições HTTP
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            // Configura o endpoint do Swagger JSON e o título do Swagger UI
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Alocação de Laboratórios API v1");
          //  options.RoutePrefix = string.Empty; // Define a raiz para acessar o Swagger UI
        });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
