using ControleTarefas.Api.Middleware;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Negocio.Negocios;
using ControleTarefas.Repositorio;
using ControleTarefas.Repositorio.Interface;
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

            services.AddScoped<IGerenciadorTransacao, GerenciadorTransacao>();
        }

        private static void InjetarNegocio(IServiceCollection services)
        {
            services.AddScoped<ITarefaNegocio, TarefaNegocio>();
            services.AddScoped<IUsuarioNegocio, UsuarioNegocio>();
            services.AddScoped<IAtribuirTarefaNegocio, AtribuirTarefaNegocio>();
        }

        private static void InjetarRepositorio(IServiceCollection services)
        {
            services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        }
    }
}
