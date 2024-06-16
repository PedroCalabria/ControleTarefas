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

        public Task<List<TarefaDTO>> ListarTarefas(List<string> tarefas)
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

        public async Task<List<TarefaDTO>> InserirTarefa(CadastroTarefaModel novaTarefa)
        {
            ValidarTarefa(novaTarefa.Titulo);
            var tarefa = await _tarefaRepositorio.ObterTarefa(novaTarefa.Titulo);

            if (tarefa != null)
            {
                var titulo = tarefa.Titulo;
                throw new BusinessException(string.Format(BusinessMessages.RegistroJaExistente, titulo));
            }

            //CadastroTarefaValidator.Validar(novaTarefa, tarefa);

            await _tarefaRepositorio.Inserir(new Tarefa(novaTarefa.Titulo));
            _log.InfoFormat(BusinessMessages.OperacaoRealizadaComSucesso, "InserirTarefa");

            return await _tarefaRepositorio.ListarTodas();
        }

        public async Task<List<TarefaDTO>> DeletarTarefa(string nomeTarefa)
        {
            ValidarTarefa(nomeTarefa);
            var tarefa = await _tarefaRepositorio.ObterTarefa(nomeTarefa);

            if (tarefa != null)
            {
                await _tarefaRepositorio.Deletar(tarefa);
                _log.InfoFormat(BusinessMessages.OperacaoRealizadaComSucesso, "DeletarTarefa");
            }
            else
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, nomeTarefa);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, nomeTarefa));
            }

            return await _tarefaRepositorio.ListarTodas();
        }

        public async Task<List<TarefaDTO>> AlterarTarefa(string nomeTarefa, string novaTarefa)
        {
            ValidarTarefa(novaTarefa);
            var tarefa = await _tarefaRepositorio.ObterTarefa(nomeTarefa);

            if (tarefa == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, nomeTarefa);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, nomeTarefa));
            }

            tarefa.Titulo = novaTarefa;
            await _tarefaRepositorio.Atualizar(tarefa);
            _log.InfoFormat(BusinessMessages.OperacaoRealizadaComSucesso, "AlterarTarefa");

            return await _tarefaRepositorio.ListarTodas();
        }
        private static void ValidarTarefa(string titulo)
        {
            if (string.IsNullOrEmpty(titulo))
                throw new BusinessException(string.Format(BusinessMessages.CampoObrigatorio, "Título"));
        }
    }
}
