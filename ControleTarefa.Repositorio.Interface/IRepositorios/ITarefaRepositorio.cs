using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;

namespace ControleTarefas.Repositorio.Interface.IRepositorios
{
    public interface ITarefaRepositorio
    {

        List<TarefaDTO> ListarTarefas(List<string> tarefas);

        List<TarefaDTO> ListarTodas();

        void InserirTarefa(Tarefa novaTarefa);

        void DeletarTarefa(Tarefa tarefa);

        Tarefa? ObterTarefa(string tituloTarefa);
    }
}