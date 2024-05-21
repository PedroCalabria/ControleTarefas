using Microsoft.AspNetCore.Mvc;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Entidade.DTO;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Entidade.Model;
using ControleTarefas.Negocio.Negocios;

namespace ControleTarefas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController
    {
        private readonly IUsuarioNegocio _usuarioNegocio;

        public UsuarioController(IUsuarioNegocio usuarioNegocio)
        {
            _usuarioNegocio = usuarioNegocio;
        }

        [HttpPost("InserirUsuario")]
        public ActionResult<List<UsuarioDTO>> InserirUsuario(CadastroUsuarioModel novoUsuario)
        {
            return _usuarioNegocio.InserirUsuario(novoUsuario);
        }

        [HttpGet("ListarTodosUsuarios")]
        public ActionResult<List<UsuarioDTO>> ListarTodosUsuarios()
        {
            return _usuarioNegocio.ListarUsuarios(null);
        }

        [HttpPost("FiltrarUsuarios")]
        public ActionResult<List<UsuarioDTO>> FiltrarUsuarios(List<string> emails)
        {
            return _usuarioNegocio.ListarUsuarios(emails);
        }

        [HttpDelete("DeletarUsuario")]
        public ActionResult<List<UsuarioDTO>> DeletarUsuario(string email)
        {
            return _usuarioNegocio.DeletarUsuario(email);
        }

        [HttpPut("AlterarUsuario")]
        public ActionResult<List<UsuarioDTO>> AlterarTarefa(string email, CadastroUsuarioModel novoUsuario)
        {
            return _usuarioNegocio.AlterarUsuario(email, novoUsuario);
        }
    }
}
