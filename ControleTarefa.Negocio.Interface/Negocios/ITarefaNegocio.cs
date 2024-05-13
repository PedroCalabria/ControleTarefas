using ControleTarefas.Entidade.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Negocio.Interface.Negocios
{
    public interface ITarefaNegocio
    {
        List<TarefaDTO> ListarTarefas(List<string> tarefas);

        List<TarefaDTO> InserirTarefa(string novaTarefa);

        List<TarefaDTO> DeletarTarefa(string nomeTarefa);

        List<TarefaDTO> AlterarTarefa(string nomeTarefa, string novoNomeTarefa);
    }
}
