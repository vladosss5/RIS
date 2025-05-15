using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RP.Application.Extentions;

public static class DIExtentions
{
    /// <summary>
    /// Натсройка подключения к БД, связывание интерфейсов с реализациейми.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns>Сервисы с контекстом БД и связанными интерфейсами</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DbContext>();
        
        return services;
    }
}