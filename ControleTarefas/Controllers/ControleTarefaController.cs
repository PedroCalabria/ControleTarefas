using Microsoft.AspNetCore.Mvc;

namespace ControleTarefas.Controllers
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

        //Construtor da classe ControleTarefaController.
        public ControleTarefaController(ILogger<ControleTarefaController> logger)
        {
            _logger = logger;
        }

        // Indica o tipo de requisição
        [HttpGet("ListarTodasTarefas")]

        // ActionResult => retorno de método usado em controladores do ASP.NET Core MVC para representar o resultado de uma ação.
        // Fornece flexibilidade ao controlador para retornar diferentes tipos de respostas HTTP, como um objeto JSON.
        public ActionResult<List<string>> Get()
        {
            return Tarefas;
        }

        [HttpPost("InserirTarefa")]
        public ActionResult<List<string>> Post(string novaTarefa)
        {
            var timestamp = DateTime.Now.ToString("yyyy/MM/dd - HH:mm:ss");

            if (Tarefas.Contains(novaTarefa))
            {
                var msg = $"[{timestamp}] A tarefa '{novaTarefa}'  já existe ma base.";
                _logger.LogInformation(msg);
                throw new BusinessException(msg);
            }

            _logger.LogInformation($"[{timestamp}] A tarefa '{novaTarefa}' foi adicionada.");
            Tarefas.Add(novaTarefa);

            return Tarefas;
        }

        [HttpDelete("DeletarTarefa")]
        public ActionResult<List<string>> Delete(string nomeTarefa)
        {
            var timestamp = DateTime.Now.ToString("yyyy/MM/dd - HH:mm:ss");
            var indexTarefaExistente = Tarefas.FindIndex(tarefa => tarefa == nomeTarefa);

            if (indexTarefaExistente != -1)
            {
                Tarefas.Remove(nomeTarefa);
                _logger.LogInformation($"[{timestamp}] A tarefa '{nomeTarefa}' foi removida.");
            }
            else
            {
                throw new BusinessException($"[{timestamp}] A tarefa '{nomeTarefa}' não existe na base.");
            }

            return Tarefas;
        }

        [HttpPut("AlterarTarefa")]
        public ActionResult<List<string>> Put(string nomeTarefa, string novoNomeTarefa)
        {
            var timestamp = DateTime.Now.ToString("yyyy/MM/dd - HH:mm:ss");
            var indexTarefaExistente = Tarefas.FindIndex(tarefa => tarefa == nomeTarefa);

            if (indexTarefaExistente != -1)
            {
                Tarefas[indexTarefaExistente] = novoNomeTarefa;
            }
            else
            {
                throw new BusinessException($"[{timestamp}] A tarefa '{nomeTarefa}' não existe na base.");
            }

            return Tarefas;
        }
    }
}
