using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;

namespace ControleTarefas.Repositorio.Interface.IRepositorios
{
    public interface ITarefaRepositorio : IRepositorioBase<Tarefa>
    {
        Task<List<TarefaDTO>> ListarTarefas(List<string> tarefas);
        Task<List<TarefaDTO>> ListarTodas();
        Task<Tarefa> ObterTarefa(string tituloTarefa);
        Task<Tarefa> ObterTarefa(int idTarefa);
        Task<List<Tarefa>> ConsultarTarefas(List<int> idTarefas);
        Task<List<Usuario>> ObterUsuariosTarefa(int idTarefa);
        Task<List<TarefaDTO>> ConsultarTarefasPorIdUsuario(int idUsuario);
    }
}