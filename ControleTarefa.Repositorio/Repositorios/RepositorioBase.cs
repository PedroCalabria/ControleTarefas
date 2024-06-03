using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Repositorio.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : class, IEntidade
    {
        protected readonly Contexto _contexto;

        protected virtual DbSet<TEntidade> EntitySet { get; }

        public RepositorioBase(Contexto contexto)
        {
            _contexto = contexto;
            EntitySet = _contexto.Set<TEntidade>();
        }

        public async Task<TEntidade> Atualizar(TEntidade entidade)
        {
            var entityEntry = EntitySet.Update(entidade);

            await _contexto.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task Deletar(TEntidade entidade)
        {
            EntitySet.Remove(entidade);

            await _contexto.SaveChangesAsync();
        }

        public async Task DeletarPorId(object id)
        {
            var entity = await EntitySet.FindAsync(id);

            if (entity != null)
            {
                await Deletar(entity);
            }
        }

        public async Task<TEntidade> Inserir(TEntidade entidade)
        {
            var entityEntry = await EntitySet.AddAsync(entidade);

            await _contexto.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<TEntidade> ObterPorID(object id) => await EntitySet.FindAsync(id);

        public Task<List<TEntidade>> Todos()
        {
            return EntitySet.ToListAsync();
        }
    }
}
