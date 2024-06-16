using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Entidade.Model;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Negocio.Negocios;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using ControleTarefas.Repositorio.Repositorios;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Utilitarios.Messages;
using NUnit.Framework;

namespace ControleTarefas.TestesUnitarios.BancoEmMemoria
{
    public class TarefaNegocioTeste : TesteUnitarioBase
    {
        private ITarefaNegocio _negocio;
        private ITarefaRepositorio _repositorio;

        [SetUp]
        public void SetUp()
        {
            _repositorio = new TarefaRepositorio(_contexto);

            RegistrarObjeto(typeof(ITarefaRepositorio), _repositorio);

            Registrar<ITarefaNegocio, TarefaNegocio>();

            _negocio = ObterServico<ITarefaNegocio>();
        }

        [Test]
        public void InserirTarefa_Sucesso()
        {
            var tarefa = new CadastroTarefaModel { Titulo = "Titulo Tarefa" };

            async Task action() => await _negocio.InserirTarefa(tarefa);

            Assert.DoesNotThrowAsync(action);
        }

        [TestCase("")]
#pragma warning disable NUnit1001 // The individual arguments provided by a TestCaseAttribute must match the type of the corresponding parameter of the method
        [TestCase(null)]
#pragma warning restore NUnit1001 // The individual arguments provided by a TestCaseAttribute must match the type of the corresponding parameter of the method
        public void InserirTarefa_TituloInvalido(string titulo)
        {
            //Arrange
            var tarefa = new CadastroTarefaModel { Titulo = titulo };

            //Act
            async Task action() => await _negocio.InserirTarefa(tarefa);

            //Assert
            var excepetion = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(excepetion.Message == string.Format(BusinessMessages.CampoObrigatorio, "Título"));
        }

        [Test]
        public void AlterarTarefa_Sucesso()
        {
            var tarefa = new Tarefa { Titulo = "tarefa" };
            var novoNomeTarefa = "novoNome";
            _contexto.Add(tarefa);
            _contexto.SaveChanges();

            async Task action() => await _negocio.AlterarTarefa(tarefa.Titulo, novoNomeTarefa);

            Assert.DoesNotThrowAsync(action);
        }

#pragma warning disable NUnit1001 // The individual arguments provided by a TestCaseAttribute must match the type of the corresponding parameter of the method
        [TestCase(null)]
#pragma warning restore NUnit1001 // The individual arguments provided by a TestCaseAttribute must match the type of the corresponding parameter of the method
        [TestCase("")]
        public void AlterarTarefa_TituloInvalido(string novoNomeTarefa)
        {
            var titulo = "tarefaExistente";

            async Task action() => await _negocio.AlterarTarefa(titulo, novoNomeTarefa);

            var excepetion = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(excepetion.Message == string.Format(BusinessMessages.CampoObrigatorio, "Título"));
        }

        [Test]
        public void DeletarTarefa_Sucesso()
        {
            var tarefa = new Tarefa { Titulo = "tarefa" };
            var nomeTarefa = "tarefa";
            _contexto.Add(tarefa);
            _contexto.SaveChanges();

            async Task action() => await _negocio.DeletarTarefa(nomeTarefa);

            Assert.DoesNotThrowAsync(action);
        }

        [Test]
        public void DeletarTarefa_Inexistente()
        {
            var nomeTarefa = "tarefa";

            async Task action() => await _negocio.DeletarTarefa(nomeTarefa);

            var excepetion = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(excepetion.Message == string.Format(BusinessMessages.RegistroNaoEncontrado, nomeTarefa));
        }

        [TestCase("")]
#pragma warning disable NUnit1001 // The individual arguments provided by a TestCaseAttribute must match the type of the corresponding parameter of the method
        [TestCase(null)]
#pragma warning restore NUnit1001 // The individual arguments provided by a TestCaseAttribute must match the type of the corresponding parameter of the method
        public void DeletarTarefa_TituloInvalido(string nomeTarefa)
        {
            async Task action() => await _negocio.DeletarTarefa(nomeTarefa);

            var excepetion = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(excepetion.Message == string.Format(BusinessMessages.CampoObrigatorio, "Título"));
        }
    }
}