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

            if (verificarTarefaExistente(novaTarefa))
            {
                var msg = $"[{timestamp}] A tarefa '{novaTarefa}'  já existe na base.";
                throw new BusinessException(msg);
            }

            return _tarefaRepositorio.InserirTarefa(novaTarefa);
        }

        public List<TarefaDTO> DeletarTarefa(string nomeTarefa)
        {
            var timestamp = DateTime.Now.ToString("yyyy/MM/dd - HH:mm:ss");

            if (verificarTarefaExistente(nomeTarefa))
            {
                _tarefaRepositorio.DeletarTarefa(nomeTarefa.ToUpper());
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

            if (!verificarTarefaExistente(nomeTarefa))
            {
                throw new BusinessException($"[{timestamp}] A tarefa '{nomeTarefa}' não existe na base.");
            }

            return _tarefaRepositorio.AlterarTarefa(nomeTarefa.ToUpper(), novoNomeTarefa);
        }

        private bool verificarTarefaExistente(string nomeTarefa)
        {
            List<string> tarefas = new List<string>
            {
                nomeTarefa.ToUpper()
            };

            List<TarefaDTO> tarefaExistente = _tarefaRepositorio.ListarTarefas(tarefas);

            if (tarefaExistente.Count <= 0)
            {
                return false;
            }

            return true;
        }

    }
}
