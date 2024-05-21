using ControleTarefas.Api.Middleware;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Negocio.Negocios;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using ControleTarefas.Repositorio.Repositorios;
using ControleTarefas.Validator.Fluent;
using FluentValidation.AspNetCore;

namespace ControleTarefas.Api.Configuration
{
    public static class FluentConfiguration
    {
        public static void AddFluentConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<CadastroTarefaValidator>());
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<CadastroUsuarioValidator>());
        }
    }
}