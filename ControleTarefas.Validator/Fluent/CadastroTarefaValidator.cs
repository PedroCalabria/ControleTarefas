using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Entidade.Model;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Utilitarios.Messages;
using FluentValidation;
using System.Net.Http.Headers;

namespace ControleTarefas.Validator.Fluent
{
    public class CadastroTarefaValidator : AbstractValidator<CadastroTarefaModel>
    {
        public CadastroTarefaValidator()
        {
            RuleFor(tarefa => tarefa.Titulo)
                .NotNull().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "Título"))
                .NotEmpty().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "Título"))
                .MinimumLength(5).WithMessage(string.Format(BusinessMessages.CampoTamanhoMinimo, "Título", 5))
                .MaximumLength(50).WithMessage(string.Format(BusinessMessages.CampoTamanhoMaximo, "Título", 50));
        }
    }
}
