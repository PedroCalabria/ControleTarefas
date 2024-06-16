using ControleTarefas.Entidade.Entidades;
using ControleTarefas.Entidade.Enum;
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
    public class UsuarioNegocioTeste : TesteUnitarioBase
    {
        private IUsuarioNegocio _negocio;
        private IUsuarioRepositorio _repositorio;

        [SetUp]
        public void SetUp()
        {
            _repositorio = new UsuarioRepositorio(_contexto);

            RegistrarObjeto(typeof(IUsuarioRepositorio), _repositorio);

            Registrar<IUsuarioNegocio, UsuarioNegocio>();

            _negocio = ObterServico<IUsuarioNegocio>();
        }

        [TestCase(PerfilEnum.Professor)]
        public void InserirUsuario_Sucesso(PerfilEnum perfil)
        {
            var novoUsuario = new CadastroUsuarioModel
            {
                Email = "null",
                Login = "Login",
                Nome = "Nome",
                Perfil = perfil
            };

            async Task action() => await _negocio.InserirUsuario(novoUsuario);

            Assert.DoesNotThrowAsync(action);
        }

        [TestCase(PerfilEnum.Aluno)]
        [TestCase(PerfilEnum.Professor)]
        public void InserirUsuario_UsuarioExistente(PerfilEnum perfil)
        {
            var usuarioExistente = new Usuario
            {
                Email = "Email",
                Login = "Login",
                Nome = "Nome",
                Perfil = perfil
            };

            var novoUsuario = new CadastroUsuarioModel
            {
                Email = "Email",
                Login = "Login",
                Nome = "Nome",
                Perfil = perfil
            };

            _contexto.Add(usuarioExistente);
            _contexto.SaveChanges();

            async Task action() => await _negocio.InserirUsuario(novoUsuario);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == string.Format(BusinessMessages.RegistroJaExistente, usuarioExistente.Email));
        }
    }
}