using ControleTarefas.Entidade.Model;
using ControleTarefas.Utilitarios.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Validator.Fluent
{
    public class AtribuirTarefaValidator : AbstractValidator<AtribuirTarefaModel>
    {
        public AtribuirTarefaValidator()
        {
            RuleFor(tarefa => tarefa.idTarefas)
            .NotNull().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "idTarefas"))
            .NotEmpty().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "idTarefas"))
            .Must(ids => ids.TrueForAll(id => id > 0)).WithMessage(BusinessMessages.IdValorMinimo);

            RuleFor(tarefa => tarefa.idUsuario)
                .GreaterThan(0).WithMessage(string.Format(BusinessMessages.IdValorMinimo))
                .NotNull().WithMessage(string.Format(BusinessMessages.CampoObrigatorio, "idUsuario"));
        }
    }
}
