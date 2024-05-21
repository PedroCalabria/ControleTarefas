using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Entidade.Enum;
using ControleTarefas.Entidade.Model;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Utilitarios.Messages;
using FluentValidation;
using System.Net.Http.Headers;

namespace ControleTarefas.Validator.Fluent
{
    public class CadastroUsuarioValidator : AbstractValidator<CadastroUsuarioModel>
    {
        public CadastroUsuarioValidator()
        {
            RuleFor(tarefa => tarefa.Nome)
                .NotNull().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "Nome"))
                .NotEmpty().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "Nome"));

            RuleFor(tarefa => tarefa.Email).EmailAddress().WithMessage(string.Format(BusinessMessages.CampoInvalido, "Email"))
                .NotNull().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "Email"))
                .NotEmpty().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "Email"));

            RuleFor(tarefa => tarefa.Perfil)
                .Must(perfil => Enum.IsDefined(typeof(PerfilEnum), perfil)).WithMessage(string.Format(BusinessMessages.CampoInvalido, "Perfil"))
                .NotNull().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "Perfil"));
        }
    }
}
