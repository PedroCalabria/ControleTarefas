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
        List<UsuarioDTO> InserirUsuario(CadastroUsuarioModel email);
        List<UsuarioDTO> ListarUsuarios(List<string> emails);
        List<UsuarioDTO> DeletarUsuario(string email);
        List<UsuarioDTO> AlterarUsuario(string email, CadastroUsuarioModel novoUsuario);
    }
}
