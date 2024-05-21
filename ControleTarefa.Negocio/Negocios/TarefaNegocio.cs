using ControleTarefas.Entidade.DTO;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Entidade.Entidades;
using log4net;
using ControleTarefas.Utilitarios.Messages;
using ControleTarefas.Validator.Manual;
using ControleTarefas.Entidade.Model;


namespace ControleTarefas.Negocio.Negocios
{
    public class TarefaNegocio : ITarefaNegocio
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(TarefaNegocio));
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaNegocio(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        public List<TarefaDTO> ListarTarefas(List<string> tarefas)
        {
            if (tarefas == null)
            {
                return _tarefaRepositorio.ListarTodas();
            }
            else
            {
                tarefas = tarefas.Select(tarefa => tarefa.ToUpper())
                                 .ToList();

                return _tarefaRepositorio.ListarTarefas(tarefas);
            }
        }

        public List<TarefaDTO> InserirTarefa(CadastroTarefaModel novaTarefa)
        {
            var tarefa = _tarefaRepositorio.ObterTarefa(novaTarefa.Titulo);

            CadastroTarefaValidator.Validar(novaTarefa, tarefa);

            _tarefaRepositorio.InserirTarefa(new Tarefa(novaTarefa.Titulo));
            _log.InfoFormat(BusinessMessages.OperacaoRealizadaComSucesso, "InserirTarefa");

            return _tarefaRepositorio.ListarTodas();
        }

        public List<TarefaDTO> DeletarTarefa(string nomeTarefa)
        {
            var tarefa = _tarefaRepositorio.ObterTarefa(nomeTarefa);

            if (tarefa != null)
            {
                _tarefaRepositorio.DeletarTarefa(tarefa);
                _log.InfoFormat(BusinessMessages.OperacaoRealizadaComSucesso, "DeletarTarefa");
            }
            else
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, nomeTarefa);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, nomeTarefa));
            }

            return _tarefaRepositorio.ListarTodas();
        }

        public List<TarefaDTO> AlterarTarefa(string nomeTarefa, CadastroTarefaModel novaTarefa)
        {
            var tarefa = _tarefaRepositorio.ObterTarefa(nomeTarefa);

            if (tarefa == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, nomeTarefa);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, nomeTarefa));
            }

            tarefa.Titulo = novaTarefa.Titulo;
            _log.InfoFormat(BusinessMessages.OperacaoRealizadaComSucesso, "AlterarTarefa");

            return _tarefaRepositorio.ListarTodas();
        }      

    }
}
