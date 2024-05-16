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

        public void InserirTarefa(Tarefa novaTarefa)
        {
            Tarefas.Add(novaTarefa);
        }

        public void DeletarTarefa(Tarefa tarefa)
        {

            Tarefas.Remove(tarefa);
        }

        public Tarefa? ObterTarefa(string nomeTarefa)
        {
            return Tarefas.Find(tarefa => tarefa.Titulo.ToUpper() == nomeTarefa.ToUpper());
        }
    }
}
