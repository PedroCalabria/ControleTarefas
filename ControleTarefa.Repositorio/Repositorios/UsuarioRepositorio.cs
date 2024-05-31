using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Entidade.Enum;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using Microsoft.EntityFrameworkCore;

namespace ControleTarefas.Repositorio.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(Contexto contexto) : base(contexto) { }

        public Task<List<UsuarioDTO>> ListarTodos()
        {
            var query = EntitySet.OrderBy(usuario => usuario.Nome)
                           .Distinct()
                           .Select(usuario => new UsuarioDTO
                           {
                               Nome = usuario.Nome,
                               Email = usuario.Email,
                               Perfil = usuario.Perfil,
                               DataAtualizacao = usuario.DataAtualizacao
                           });

            return query.ToListAsync();
        }

        public Task<List<UsuarioDTO>> ListarUsuarios(List<string> emails)
        {
            var query = EntitySet.Where(usuario => emails.Contains(usuario.Email.ToUpper()))
                           .OrderBy(usuario => usuario.Nome)
                           .Distinct()
                           .Select(usuario => new UsuarioDTO
                           {
                               Nome = usuario.Nome,
                               Email = usuario.Email,
                               Perfil = usuario.Perfil,
                               DataAtualizacao = usuario.DataAtualizacao
                           });

            return query.ToListAsync();
        }

        public Task<Usuario> ObterUsuario(string email)
        {
            var query = EntitySet.Where(usuario => usuario.Email.ToLower() == email.ToLower());

            return query.FirstOrDefaultAsync();
        }

        public Task<Usuario> ObterUsuario(int IdUsuario)
        {
            var query = EntitySet.Include(e => e.TarefasUsuario)
                .Where(usuario => usuario.Id == IdUsuario);

            return query.FirstOrDefaultAsync();
        }

        public Task<List<Usuario>> ConsultarUsuarios(List<int> idsUsuarios)
        {
            var query = EntitySet.Where(e => idsUsuarios.Contains(e.Id));

            return query.ToListAsync();
        }
    }
}
