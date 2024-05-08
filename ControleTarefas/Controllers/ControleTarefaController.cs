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
        private static List<string> TitulosTarefas { get; set; } = new() { "Instalação", "Configuração", "Criar Projeto", "Exercício Prático" };

        // Indica o tipo de requisição
        [HttpGet]

        // ActionResult => retorno de método usado em controladores do ASP.NET Core MVC para representar o resultado de uma ação.
        // Fornece flexibilidade ao controlador para retornar diferentes tipos de respostas HTTP, como um objeto JSON.
        public ActionResult<List<string>> Get()
        {
            return TitulosTarefas;
        }

        [HttpPost]
        public ActionResult<List<string>> Post(string novaTarefa)
        {
            TitulosTarefas.Add(novaTarefa);
            return TitulosTarefas;
        }

        [HttpDelete]
        public ActionResult<List<string>> Delete(string tarefa) {
            TitulosTarefas.Remove(tarefa);
            return TitulosTarefas;
        }

        [HttpPut]
        public ActionResult<List<string>> Put(string nomeTarefa, string novoNomeTarefa)
        {
            var indexTarefaExistente = TitulosTarefas.FindIndex(tarefa => tarefa == nomeTarefa);

            TitulosTarefas[indexTarefaExistente] = novoNomeTarefa;

            return TitulosTarefas;
        }
    }
}
