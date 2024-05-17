using Microsoft.AspNetCore.Mvc;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Entidade.DTO;
using ControleTarefas.Utilitarios.Exceptions;

namespace ControleTarefas.Api.Controllers
{
    // Anotação (attribute) que indica que a classe ControleTarefaController é um controlador de API.
    // Habilita comportamentos específicos para APIs.
    [ApiController]

    // Define o padrão de rota para as ações dentro do controlador.
    [Route("api/[controller]")]
    public class ControleTarefaController
    {
        // { get; set; } define o getter e setter automaticamente.
        private static List<string> Tarefas { get; set; } = new() { "Instalação", "Configuração", "Criar Projeto", "Exercício Prático" };

        // O modificador readonly garante que _logger só pode ser atribuído uma vez.
        private readonly ILogger<ControleTarefaController> _logger;

        private readonly ITarefaNegocio _tarefaNegocio;

        //Construtor da classe ControleTarefaController.
        public ControleTarefaController(ILogger<ControleTarefaController> logger, ITarefaNegocio tarefaNegocio)
        {
            _logger = logger;
            _tarefaNegocio = tarefaNegocio;
        }

        // Indica o tipo de requisição
        [HttpGet("ListarTodasTarefas")]

        // ActionResult => retorno de método usado em controladores do ASP.NET Core MVC para representar o resultado de uma ação.
        // Fornece flexibilidade ao controlador para retornar diferentes tipos de respostas HTTP, como um objeto JSON.
        public ActionResult<List<TarefaDTO>> ListarTodasTarefas()
        {
            return _tarefaNegocio.ListarTarefas(null);
        }

        [HttpPost("FiltrarTarefas")]
        public ActionResult<List<TarefaDTO>> FiltrarTarefas(List<string> tarefas)
        {
            return _tarefaNegocio.ListarTarefas(tarefas);
        }

        [HttpPost("InserirTarefa")]
        public ActionResult<List<TarefaDTO>> InserirTarefa(string novaTarefa)
        {
            return _tarefaNegocio.InserirTarefa(novaTarefa);
        }

        [HttpDelete("DeletarTarefa")]
        public ActionResult<List<TarefaDTO>> DeletarTarefa(string nomeTarefa)
        {
           return _tarefaNegocio.DeletarTarefa(nomeTarefa);
        }

        [HttpPut("AlterarTarefa")]
        public ActionResult<List<TarefaDTO>> AlterarTarefa(string nomeTarefa, string novoNomeTarefa)
        {
            return _tarefaNegocio.AlterarTarefa(nomeTarefa, novoNomeTarefa);
        }
    }
}
