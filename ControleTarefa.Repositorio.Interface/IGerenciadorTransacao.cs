using System.Data;

namespace ControleTarefas.Repositorio.Interface
{
    public interface IGerenciadorTransacao
    {
        Task BeginTransactionAsync(IsolationLevel isolationLevel);

        Task CommitTransactionAsync();

        Task RollbackTransactionsAsync();
    }
}
