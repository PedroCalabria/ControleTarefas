using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Negocio.Interface.Negocios
{
    public interface IUsuarioNegocio
    {
        Task<List<UsuarioDTO>> InserirUsuario(CadastroUsuarioModel email);
        Task<List<UsuarioDTO>> ListarUsuarios(List<string> emails);
        Task<List<UsuarioDTO>> DeletarUsuario(string email);
        Task<List<UsuarioDTO>> AlterarUsuario(string email, CadastroUsuarioModel novoUsuario);
    }
}
