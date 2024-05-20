using ControleTarefas.Api.Middleware;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Negocio.Negocios;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using ControleTarefas.Repositorio.Repositorios;

namespace ControleTarefas.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            InjetarRepositorio(services);
            InjetarNegocio(services);
        }

        private static void InjetarNegocio(IServiceCollection services)
        {
            services.AddScoped<ITarefaNegocio, TarefaNegocio>();
            services.AddScoped<IUsuarioNegocio, UsuarioNegocio>();
        }

        private static void InjetarRepositorio(IServiceCollection services)
        {
            services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        }
    }
}
