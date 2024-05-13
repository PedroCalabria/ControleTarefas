using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;

namespace ControleTarefas.Repositorio.Interface.IRepositorios
{
    public interface ITarefaRepositorio
    {

        List<TarefaDTO> ListarTarefas(List<string> tarefas);

        List<TarefaDTO> ListarTodas();

        List<TarefaDTO> InserirTarefa(string tarefa);

        List<TarefaDTO> DeletarTarefa(string nomeTarefa);

        List<TarefaDTO> AlterarTarefa(string nomeTarefa, string novoNomeTarefa);
    }
}