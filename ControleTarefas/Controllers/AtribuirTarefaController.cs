using Microsoft.AspNetCore.Mvc;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Entidade.DTO;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Entidade.Model;
using ControleTarefas.Utilitarios;
using ControleTarefas.Negocio.Negocios;

namespace ControleTarefas.Api.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class AtribuirTarefaController
    {
        private readonly IAtribuirTarefaNegocio _atribuirTarefaNegocio;

        public AtribuirTarefaController(IAtribuirTarefaNegocio atribuirTarefaNegocio)
        {
            _atribuirTarefaNegocio = atribuirTarefaNegocio;
        }

        [HttpPost("AtribuirTarefa")]
        [TransacaoObrigatoria]

        public async Task AtribuirTarefa(AtribuirTarefaModel tarefasUsuario)
        {
            await _atribuirTarefaNegocio.AtribuirTarefa(tarefasUsuario);
        }

        [HttpGet("ObterTarefasUsuario")]

        public async Task<List<TarefaDTO>> ObterTarefasUsuario(int idUsuario)
        {
            return await _atribuirTarefaNegocio.ObterTarefasUsuario(idUsuario);
        }

        [HttpGet("ObterUsuariosTarefa")]

        public async Task<List<UsuarioDTO>> ObterUsuariosTarefa(int idTarefa)
        {
            return await _atribuirTarefaNegocio.ObterUsuariosTarefa(idTarefa);
        }

        [HttpDelete("RemoverTarefaUsuarios")]
        [TransacaoObrigatoria]
        public async Task RemoverTarefaUsuarios(int idTarefa)
        {
            await _atribuirTarefaNegocio.RemoverTarefaUsuario(idTarefa);
        }

        [HttpDelete("RemoverTarefaUsuario")]
        [TransacaoObrigatoria]
        public async Task RemoverTarefaUsuario(int idTarefa, int idUsuario)
        {
            await _atribuirTarefaNegocio.RemoverTarefaUsuario(idTarefa, idUsuario);
        }
    }
}
