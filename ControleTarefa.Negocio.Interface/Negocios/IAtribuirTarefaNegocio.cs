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
    public interface IAtribuirTarefaNegocio
    {
        Task AtribuirTarefa(AtribuirTarefaModel tarefasUsuario);

        Task<List<UsuarioDTO>> ObterUsuariosTarefa(int idTarefa);

        Task<List<TarefaDTO>> ObterTarefasUsuario(int idUsuario);

        Task RemoverTarefaUsuario(int idTarefa);

        Task RemoverTarefaUsuario(int idTarefa, int idUsuario);
    }
}
