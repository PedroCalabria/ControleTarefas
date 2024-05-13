using ControleTarefas.Entidade.DTO;
using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Repositorio.Interface.IRepositorios;

namespace ControleTarefas.Repositorio.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private static List<Tarefa> Tarefas = new() { new("Instalação"), new("Configuração"), new("Criar Projeto"), new("Exercício Prático") };

        public List<TarefaDTO> ListarTarefas(List<string> tarefas)
        {
            return Tarefas.Where(tarefa => tarefas.Contains(tarefa.Titulo.ToUpper()))
                          .OrderBy(tarefa => tarefa.Titulo)
                          .Select(tarefa => new TarefaDTO
                          {
                              Titulo = tarefa.Titulo,
                          })
                          .ToList();
        }

        public List<TarefaDTO> ListarTodas()
        {
            return Tarefas.OrderBy(tarefa => tarefa.Titulo)
                          .Select(tarefa => new TarefaDTO
                          {
                              Titulo = tarefa.Titulo,
                          })
                          .ToList();
        }

        public List<TarefaDTO> InserirTarefa(string novaTarefa)
        {
            Tarefas.Add(new(novaTarefa));

            return Tarefas.OrderBy(tarefa => tarefa.Titulo)
                          .Select(tarefa => new TarefaDTO
                          {
                              Titulo = tarefa.Titulo,
                          })
                          .ToList();
        }

        public List<TarefaDTO> DeletarTarefa(string nomeTarefa)
        {

            Tarefas.RemoveAll(tarefa => tarefa.Titulo.ToUpper() == nomeTarefa);

            return Tarefas.OrderBy(tarefa => tarefa.Titulo.ToUpper())
                          .Select(tarefa => new TarefaDTO
                          {
                              Titulo = tarefa.Titulo,
                          })
                          .ToList();
        }

        public List<TarefaDTO> AlterarTarefa(string nomeTarefa, string novoNomeTarefa)
        {
            Tarefa tarefa = Tarefas.FirstOrDefault(tarefa => tarefa.Titulo.ToUpper() == nomeTarefa);

            tarefa.Titulo = novoNomeTarefa;

            return Tarefas.OrderBy(tarefa => tarefa.Titulo)
                          .Select(tarefa => new TarefaDTO
                          {
                              Titulo = tarefa.Titulo,
                          })
                          .ToList();
        }
    }
}
