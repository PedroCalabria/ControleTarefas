using Microsoft.AspNetCore.Mvc;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Entidade.DTO;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Entidade.Model;
using ControleTarefas.Utilitarios;

namespace ControleTarefas.Api.Controllers
{
    // Anotação (attribute) que indica que a classe ControleTarefaController é um controlador de API.
    // Habilita comportamentos específicos para APIs.
    [ApiController]

    // Define o padrão de rota para as ações dentro do controlador.
    [Route("api/[controller]")]
    public class CRUDTarefaController
    {
        // { get; set; } define o getter e setter automaticamente.
        private static List<string> Tarefas { get; set; } = new() { "Instalação", "Configuração", "Criar Projeto", "Exercício Prático" };

        // O modificador readonly garante que _logger só pode ser atribuído uma vez.
        private readonly ILogger<CRUDTarefaController> _logger;

        private readonly ITarefaNegocio _tarefaNegocio;

        //Construtor da classe ControleTarefaController.
        public CRUDTarefaController(ILogger<CRUDTarefaController> logger, ITarefaNegocio tarefaNegocio)
        {
            _logger = logger;
            _tarefaNegocio = tarefaNegocio;
        }

        // Indica o tipo de requisição
        [HttpGet("ListarTodasTarefas")]

        // Task => retorno de método usado em controladores do ASP.NET Core MVC para representar o resultado de uma ação.
        // Fornece flexibilidade ao controlador para retornar diferentes tipos de respostas HTTP, como um objeto JSON.
        public async Task<List<TarefaDTO>> ListarTodasTarefas()
        {
            return await _tarefaNegocio.ListarTarefas(null);
        }

        [HttpPost("FiltrarTarefas")]
        public async Task<List<TarefaDTO>> FiltrarTarefas(List<string> tarefas)
        {
            return await _tarefaNegocio.ListarTarefas(tarefas);
        }

        [HttpPost("InserirTarefa")]
        [TransacaoObrigatoria]
        public async Task<List<TarefaDTO>> InserirTarefa(CadastroTarefaModel novaTarefa)
        {
            return await _tarefaNegocio.InserirTarefa(novaTarefa);
        }

        [HttpDelete("DeletarTarefa")]
        [TransacaoObrigatoria]
        public async Task<List<TarefaDTO>> DeletarTarefa(string nomeTarefa)
        {
           return await _tarefaNegocio.DeletarTarefa(nomeTarefa);
        }

        [HttpPut("AlterarTarefa")]
        [TransacaoObrigatoria]
        public async Task<List<TarefaDTO>> AlterarTarefa(string nomeTarefa, string novoNomeTarefa)
        {
            return await _tarefaNegocio.AlterarTarefa(nomeTarefa, novoNomeTarefa);
        }
    }
}
