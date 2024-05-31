using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;

namespace ControleTarefas.Repositorio.Interface.IRepositorios
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {

        Task<List<UsuarioDTO>> ListarUsuarios(List<string> emails);

        Task<List<UsuarioDTO>> ListarTodos();

        Task<Usuario> ObterUsuario(string email);

        Task<Usuario> ObterUsuario(int idUsuario);

        Task<List<Usuario>> ConsultarUsuarios(List<int> idsUsuarios);
    }
}