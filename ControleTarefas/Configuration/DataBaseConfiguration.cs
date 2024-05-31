using ControleTarefas.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace ControleTarefas.Api.Configuration
{
    public static class DataBaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Contexto>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
