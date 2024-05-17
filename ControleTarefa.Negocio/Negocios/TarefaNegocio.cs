using ControleTarefas.Entidade.DTO;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Entidade.Entidades;
using log4net;
using ControleTarefas.Utilitarios.Messages;


namespace ControleTarefas.Negocio.Negocios
{
    public class TarefaNegocio : ITarefaNegocio
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(TarefaNegocio));
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaNegocio(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        public List<TarefaDTO> ListarTarefas(List<string> tarefas)
        {
            if (tarefas == null)
            {
                return _tarefaRepositorio.ListarTodas();
            }
            else
            {
                tarefas = tarefas.Select(tarefa => tarefa.ToUpper())
                                 .ToList();

                return _tarefaRepositorio.ListarTarefas(tarefas);
            }
        }

        public List<TarefaDTO> InserirTarefa(string novaTarefa)
        {
            var tarefa = _tarefaRepositorio.ObterTarefa(novaTarefa);

            if (tarefa != null)
            {
                _log.InfoFormat("A tarefa '{0}' já existe na base.", novaTarefa);
                throw new BusinessException(string.Format(BusinessMessages.RegistroJaExistente, tarefa.Titulo));
            }

            _tarefaRepositorio.InserirTarefa(new Tarefa(novaTarefa));
            _log.InfoFormat("A tarefa '{0}' foi inserida na base.", novaTarefa);

            return _tarefaRepositorio.ListarTodas();
        }

        public List<TarefaDTO> DeletarTarefa(string nomeTarefa)
        {
            var tarefa = _tarefaRepositorio.ObterTarefa(nomeTarefa);

            if (tarefa != null)
            {
                _tarefaRepositorio.DeletarTarefa(tarefa);
                _log.InfoFormat("A tarefa '{0}' foi removida da base.", tarefa.Titulo);
            }
            else
            {
                _log.InfoFormat("A tarefa '{0}' não existe na base.", nomeTarefa);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, nomeTarefa));
            }

            return _tarefaRepositorio.ListarTodas();
        }

        public List<TarefaDTO> AlterarTarefa(string nomeTarefa, string novoNomeTarefa)
        {
            var tarefa = _tarefaRepositorio.ObterTarefa(nomeTarefa);

            if (tarefa == null)
            {
                _log.InfoFormat("A tarefa '{0}' não existe na base.", nomeTarefa);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, nomeTarefa));
            }

            var nomeAnterior = tarefa.Titulo;
            tarefa.Titulo = novoNomeTarefa;
            _log.InfoFormat("o titulo da tarefa '{0}' foi atualizado para '{1}'.", nomeAnterior, novoNomeTarefa);

            return _tarefaRepositorio.ListarTodas();
        }      

    }
}
