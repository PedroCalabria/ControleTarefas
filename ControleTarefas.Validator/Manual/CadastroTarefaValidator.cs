using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Entidade.Model;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Utilitarios.Messages;
using System.Net.Http.Headers;

namespace ControleTarefas.Validator.Manual
{
    public static class CadastroTarefaValidator
    {
        public static void Validar(CadastroTarefaModel novaTarefa, Tarefa? tarefa) {

            var erros = new List<string>();
            
            if (tarefa != null)
            {
                erros.Add(string.Format(BusinessMessages.RegistroJaExistente, novaTarefa));
            }
            else
            {
                if(string.IsNullOrEmpty(novaTarefa.Titulo))
                {
                    erros.Add(string.Format(BusinessMessages.CampoObrigatorio, "Titulo"));
                }
                else
                {
                    if (novaTarefa.Titulo.Length < 5)
                    {
                        erros.Add(string.Format(BusinessMessages.CampoTamanhoMinimo, "Titulo", 5));
                    }
                    if (novaTarefa.Titulo.Length > 50)
                    {
                        erros.Add(string.Format(BusinessMessages.CampoTamanhoMaximo, "Titulo", 50));
                    }
                }
            }

            if (erros.Any())
            {
                throw new BusinessListException(erros);
            }
        }
    }
}
