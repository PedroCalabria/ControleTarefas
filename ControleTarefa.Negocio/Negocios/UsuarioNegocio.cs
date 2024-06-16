using ControleTarefas.Entidade.DTO;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Entidade.Entidades;
using log4net;
using ControleTarefas.Utilitarios.Messages;
using ControleTarefas.Entidade.Model;


namespace ControleTarefas.Negocio.Negocios
{
    public class UsuarioNegocio : IUsuarioNegocio
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(UsuarioNegocio));
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioNegocio(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<List<UsuarioDTO>> InserirUsuario(CadastroUsuarioModel novoUsuario)
        {
            var usuario = await _usuarioRepositorio.ObterUsuario(novoUsuario.Email);

            if (usuario != null)
            {
                _log.InfoFormat(BusinessMessages.RegistroJaExistente, usuario.Email);
                throw new BusinessException(string.Format(BusinessMessages.RegistroJaExistente, usuario.Email));
            }

            usuario = new Usuario
            {
                Email = novoUsuario.Email,
                Nome = novoUsuario.Nome,
                Perfil = novoUsuario.Perfil,
                DataAtualizacao = DateTime.Now,
                Login = novoUsuario.Login,
            };

            await _usuarioRepositorio.Inserir(usuario);
            _log.InfoFormat(BusinessMessages.OperacaoRealizadaComSucesso, "InserirUsuario");

            return await _usuarioRepositorio.ListarTodos();
        }

        public async Task<List<UsuarioDTO>> ListarUsuarios(List<string> emails)
        {
            if (emails == null)
            {
                return await _usuarioRepositorio.ListarTodos();
            }
            else
            {
                emails = emails.Select(email => email.ToUpper())
                    .ToList();

                return await _usuarioRepositorio.ListarUsuarios(emails);
            }
        }

        public async Task<List<UsuarioDTO>> AlterarUsuario(string email, CadastroUsuarioModel novoUsuario)
        {
            var usuario = await _usuarioRepositorio.ObterUsuario(email);

            if (usuario == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, email);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, email));
            }

            usuario.Nome = novoUsuario.Nome;
            usuario.Email = novoUsuario.Email;
            usuario.Perfil = novoUsuario.Perfil;
            usuario.DataAtualizacao = DateTime.Now;

            _log.InfoFormat(BusinessMessages.OperacaoRealizadaComSucesso, "AlterarUsuario");

            return await _usuarioRepositorio.ListarTodos();
        }

        public async Task<List<UsuarioDTO>> DeletarUsuario(string email)
        {
            var usuario = await _usuarioRepositorio.ObterUsuario(email);

            if (usuario != null)
            {
                await _usuarioRepositorio.Deletar(usuario);
                _log.InfoFormat(BusinessMessages.OperacaoRealizadaComSucesso, "DeletarUsuario");
            }
            else
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, email);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, email));
            }

            return await _usuarioRepositorio.ListarTodos();
        }
    }
}
