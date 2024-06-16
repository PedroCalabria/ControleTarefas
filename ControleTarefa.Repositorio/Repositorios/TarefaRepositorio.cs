using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using Microsoft.EntityFrameworkCore;

namespace ControleTarefas.Repositorio.Repositorios
{
    public class TarefaRepositorio : RepositorioBase<Tarefa>, ITarefaRepositorio
    {
        public TarefaRepositorio(Contexto contexto) : base(contexto) { }

        public Task<List<TarefaDTO>> ListarTarefas(List<string> tarefas)
        {
            var query = EntitySet.Where(tarefa => tarefas.Contains(tarefa.Titulo.ToUpper()))
                          .Select(tarefa => new TarefaDTO
                          {
                              Titulo = tarefa.Titulo,
                          })
                          .OrderBy(tarefa => tarefa.Titulo);

            return query.ToListAsync();
        }

        public Task<List<TarefaDTO>> ListarTodas()
        {

            var query = EntitySet.Select(tarefa => new TarefaDTO
            {
                Titulo = tarefa.Titulo,
            })
                .Distinct()
                .OrderBy(tarefa => tarefa.Titulo);

            return query.ToListAsync();
        }

        public Task<Tarefa> ObterTarefa(string tituloTarefa)
        {
            return EntitySet.Include(e => e.UsuariosTarefa).FirstOrDefaultAsync(tarefa => tarefa.Titulo.ToUpper() == tituloTarefa.ToUpper());
        }

        public Task<Tarefa> ObterTarefa(int idTarefa)
        {
            return EntitySet.Include(e => e.UsuariosTarefa).FirstOrDefaultAsync(tarefa => tarefa.Id == idTarefa);
        }

        public Task<List<Tarefa>> ConsultarTarefas(List<int> idsTarefas)
        {
            var query = EntitySet.Include(e => e.UsuariosTarefa)
                                 .Where(e => idsTarefas.Contains(e.Id));

            return query.ToListAsync();
        }

        public async Task<List<Usuario>> ObterUsuariosTarefa(int idTarefa)
        {
            var tarefa = await EntitySet.Include(e => e.UsuariosTarefa)
                .ThenInclude(e => e.Usuario)
                .FirstOrDefaultAsync(tarefa => tarefa.Id == idTarefa);

            var tarefasUsuario = tarefa.UsuariosTarefa.Select(e => e.Usuario).ToList();

            return tarefasUsuario;
        }

        public Task<List<TarefaDTO>> ConsultarTarefasPorIdUsuario(int idUsuario)
        {
            var query2 = EntitySet.Where(tarefa => tarefa.UsuariosTarefa.Any(usuarioTarefa => usuarioTarefa.IdUsuario == idUsuario));

            var query = from tarefa in _contexto.Tarefas
                        join tarefaUsuario in _contexto.TarefaUsuario
                            on tarefa.Id equals tarefaUsuario.IdTarefa
                        join usuario in _contexto.Usuario
                            on tarefaUsuario.IdUsuario equals usuario.Id
                        where usuario.Id == idUsuario
                        select new TarefaDTO
                        {
                            Titulo = tarefa.Titulo
                        };

            return query.ToListAsync();
        }
    }
}
