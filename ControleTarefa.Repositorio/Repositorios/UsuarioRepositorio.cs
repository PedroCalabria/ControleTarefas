using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Entidade.Enum;
using ControleTarefas.Repositorio.Interface.IRepositorios;

namespace ControleTarefas.Repositorio.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public static List<Usuario> Usuarios = new List<Usuario>();

        public void DeletarUsuario(Usuario usuario)
        {
            Usuarios.Remove(usuario);
        }

        public void InserirUsuario(Usuario novoUsuario)
        {
            Usuarios.Add(novoUsuario);
        }

        public List<UsuarioDTO> ListarTodos()
        {
            return Usuarios.OrderBy(usuario => usuario.Nome)
                           .Distinct()
                           .Select(usuario => new UsuarioDTO
                           {
                               Nome = usuario.Nome,
                               Email = usuario.Email,
                               Perfil = usuario.Perfil,
                               DataAtualizacao = usuario.DataAtualizacao
                           })
                           .ToList();
        }

        public List<UsuarioDTO> ListarUsuarios(List<string> emails)
        {
            return Usuarios.Where(usuario => emails.Contains(usuario.Email.ToUpper()))
                           .OrderBy(usuario => usuario.Nome)
                           .Distinct()
                           .Select(usuario => new UsuarioDTO
                           {
                               Nome = usuario.Nome,
                               Email = usuario.Email,
                               Perfil = usuario.Perfil,
                               DataAtualizacao = usuario.DataAtualizacao
                           })
                           .ToList();
        }

        public Usuario? ObterUsuario(string email)
        {
            return Usuarios.Find(usuario => usuario.Email.ToLower() == email.ToLower());
        }
    }
}
