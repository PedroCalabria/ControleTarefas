using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;

namespace ControleTarefas.Repositorio.Interface.IRepositorios
{
    public interface IUsuarioRepositorio
    {

        List<UsuarioDTO> ListarUsuarios(List<string> emails);

        List<UsuarioDTO> ListarTodos();

        void InserirUsuario(Usuario novoUsuario);

        void DeletarUsuario(Usuario usuario);

        Usuario? ObterUsuario(string email);
    }
}