using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Entidade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Negocio.Interface.Negocios
{
    public interface ITarefaNegocio
    {
        Task<List<TarefaDTO>> ListarTarefas(List<string> tarefas);

        Task<List<TarefaDTO>> InserirTarefa(CadastroTarefaModel novaTarefa);

        Task<List<TarefaDTO>> DeletarTarefa(string nomeTarefa);

        Task<List<TarefaDTO>> AlterarTarefa(string nomeTarefa, string novaTarefa);
    }
}
