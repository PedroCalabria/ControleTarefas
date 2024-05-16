using ControleTarefas.Entidade.DTO;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using ControleTarefas.GenericException.GenericExceptions;
using ControleTarefas.Entidade.Entidades;


namespace ControleTarefas.Negocio.Negocios
{
    public class TarefaNegocio : ITarefaNegocio
    {
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
            var timestamp = DateTime.Now.ToString("yyyy/MM/dd - HH:mm:ss");
            var tarefa = _tarefaRepositorio.ObterTarefa(novaTarefa);

            if (tarefa != null)
            {
                var msg = $"[{timestamp}] A tarefa '{novaTarefa}'  já existe na base.";
                throw new BusinessException(msg);
            }

            _tarefaRepositorio.InserirTarefa(new Tarefa(novaTarefa));

            return _tarefaRepositorio.ListarTodas();
        }

        public List<TarefaDTO> DeletarTarefa(string nomeTarefa)
        {
            var timestamp = DateTime.Now.ToString("yyyy/MM/dd - HH:mm:ss");
            var tarefa = _tarefaRepositorio.ObterTarefa(nomeTarefa);

            if (tarefa != null)
            {
                _tarefaRepositorio.DeletarTarefa(tarefa);
            }
            else
            {
                throw new BusinessException($"[{timestamp}] A tarefa '{nomeTarefa}' não existe na base.");
            }

            return _tarefaRepositorio.ListarTodas();
        }

        public List<TarefaDTO> AlterarTarefa(string nomeTarefa, string novoNomeTarefa)
        {
            var timestamp = DateTime.Now.ToString("yyyy/MM/dd - HH:mm:ss");
            var tarefa = _tarefaRepositorio.ObterTarefa(nomeTarefa);

            if (tarefa == null)
            {
                throw new BusinessException($"[{timestamp}] A tarefa '{nomeTarefa}' não existe na base.");
            }

            tarefa.Titulo = novoNomeTarefa;

            return _tarefaRepositorio.ListarTodas();
        }      

    }
}
