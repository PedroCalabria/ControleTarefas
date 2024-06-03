using ControleTarefas.Entidade.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Repositorio.Interface.IRepositorios
{
    public interface IRepositorioBase<TEntidade> where TEntidade : class, IEntidade
    {
        Task<TEntidade> ObterPorID(object id);

        Task<List<TEntidade>> Todos();

        Task<TEntidade> Inserir(TEntidade entidade);

        Task<TEntidade> Atualizar(TEntidade entidade);

        Task Deletar(TEntidade entidade);

        Task DeletarPorId(object id);
    }
}
