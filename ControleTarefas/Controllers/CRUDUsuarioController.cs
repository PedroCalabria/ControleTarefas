using Microsoft.AspNetCore.Mvc;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Entidade.DTO;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Entidade.Model;
using ControleTarefas.Negocio.Negocios;
using ControleTarefas.Utilitarios;

namespace ControleTarefas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CRUDUsuarioController
    {
        private readonly IUsuarioNegocio _usuarioNegocio;

        public CRUDUsuarioController(IUsuarioNegocio usuarioNegocio)
        {
            _usuarioNegocio = usuarioNegocio;
        }

        [HttpPost("InserirUsuario")]
        [TransacaoObrigatoria]
        public async Task<List<UsuarioDTO>> InserirUsuario(CadastroUsuarioModel novoUsuario)
        {
            return await _usuarioNegocio.InserirUsuario(novoUsuario);
        }

        [HttpGet("ListarTodosUsuarios")]
        public async Task<List<UsuarioDTO>> ListarTodosUsuarios()
        {
            return await _usuarioNegocio.ListarUsuarios(null);
        }

        [HttpPost("FiltrarUsuarios")]
        public async Task<List<UsuarioDTO>> FiltrarUsuarios(List<string> emails)
        {
            return await _usuarioNegocio.ListarUsuarios(emails);
        }

        [HttpDelete("DeletarUsuario")]
        [TransacaoObrigatoria]
        public async Task<List<UsuarioDTO>> DeletarUsuario(string email)
        {
            return await _usuarioNegocio.DeletarUsuario(email);
        }

        [HttpPut("AlterarUsuario")]
        [TransacaoObrigatoria]
        public async Task<List<UsuarioDTO>> AlterarTarefa(string email, CadastroUsuarioModel novoUsuario)
        {
            return await _usuarioNegocio.AlterarUsuario(email, novoUsuario);
        }
    }
}
