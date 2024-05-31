using ControleTarefas.Entidade.DTO;
using ControleTarefas.Negocio.Interface.Negocios;
using ControleTarefas.Repositorio.Interface.IRepositorios;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Entidade.Entidades;
using log4net;
using ControleTarefas.Utilitarios.Messages;
using ControleTarefas.Validator.Manual;
using ControleTarefas.Entidade.Model;
using System;

namespace ControleTarefas.Negocio.Negocios
{
    public class AtribuirTarefaNegocio : IAtribuirTarefaNegocio
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(TarefaNegocio));
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public AtribuirTarefaNegocio(IUsuarioRepositorio usuarioRepositorio, ITarefaRepositorio tarefaRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _tarefaRepositorio = tarefaRepositorio;
        }

        public async Task AtribuirTarefa(AtribuirTarefaModel tarefasUsuario)
        {
            var usuario = await _usuarioRepositorio.ObterUsuario(tarefasUsuario.idUsuario);

            if (usuario == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, string.Format("usuario: {0}", tarefasUsuario.idUsuario));
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, string.Format("usuario: {0}", tarefasUsuario.idUsuario)));
            }

            var tarefas = await _tarefaRepositorio.ConsultarTarefas(tarefasUsuario.idTarefas);

            if (!tarefas.Any())
            {
                var ids = tarefasUsuario.idTarefas.ToArray();
                var stringIds = string.Join(", ", ids);
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, string.Format("tarefa: {0}", stringIds));
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, string.Format("tarefa(s): {0}", stringIds)));
            }

            foreach (var tarefa in tarefas)
            {
                if (!usuario.TarefasUsuario.Exists(e => tarefa.Id == e.IdTarefa))
                {
                    var tarefaUsuario = new TarefaUsuario
                    {
                        IdTarefa = tarefa.Id,
                        IdUsuario = usuario.Id,
                    };

                    usuario.TarefasUsuario.Add(tarefaUsuario);
                }
            }


        }

        public async Task<List<TarefaDTO>> ObterTarefasUsuario(int idUsuario)
        {
            var usuario = await _usuarioRepositorio.ObterUsuario(idUsuario);

            if (usuario == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, string.Format("usuario: {0}", idUsuario));
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, string.Format("usuario: {0}", idUsuario)));
            }

            var tarefasIds = usuario.TarefasUsuario.Select(e => e.IdTarefa)
                                           .ToList();

            var tarefas = await _tarefaRepositorio.ConsultarTarefas(tarefasIds);

            return tarefas.Select(e => new TarefaDTO
            {
                Titulo = e.Titulo,
            })
                .ToList();
        }

        public async Task<List<UsuarioDTO>> ObterUsuariosTarefa(int idTarefa)
        {
            var tarefa = await _tarefaRepositorio.ObterTarefa(idTarefa);

            if (tarefa == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, string.Format("tarefa: {0}", idTarefa));
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, string.Format("tarefa: {0}", idTarefa)));
            }

            var usuariosIds = tarefa.UsuariosTarefa.Select(e => e.IdUsuario)
                                           .ToList();

            var usuarios = await _usuarioRepositorio.ConsultarUsuarios(usuariosIds);

            return usuarios.Select(e => new UsuarioDTO
            {
                Nome = e.Nome,
                Email = e.Email,
                Perfil = e.Perfil,
                DataAtualizacao = e.DataAtualizacao
            })
                .ToList();
        }

        public async Task RemoverTarefaUsuario(int idTarefa)
        {
            var tarefa = await _tarefaRepositorio.ObterTarefa(idTarefa);

            if (tarefa == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, string.Format("tarefa: {0}", idTarefa));
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, string.Format("tarefa: {0}", idTarefa)));
            }

            var tarefaUsuario = tarefa.UsuariosTarefa.FirstOrDefault(e => e.IdTarefa == idTarefa);
            var idsUsuarios = tarefa.UsuariosTarefa.Select(e => e.IdUsuario).ToList();

            foreach(var idUsuario in idsUsuarios)
            {
                var usuario = await _usuarioRepositorio.ObterUsuario(idUsuario);
                usuario.TarefasUsuario.Remove(tarefaUsuario);
            }

            tarefa.UsuariosTarefa.Clear();
        }

        public async Task RemoverTarefaUsuario(int idTarefa, int idUsuario)
        {
            var tarefa = await _tarefaRepositorio.ObterTarefa(idTarefa);

            if (tarefa == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, string.Format("tarefa: {0}", idTarefa));
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, string.Format("tarefa: {0}", idTarefa)));
            }

            var usuario = await _usuarioRepositorio.ObterUsuario(idUsuario);

            if (usuario == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, string.Format("usuario: {0}", idUsuario));
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, string.Format("usuario: {0}", idUsuario)));
            }

            var tarefaUsuario = tarefa.UsuariosTarefa.FirstOrDefault(e => e.IdTarefa == idTarefa);
            
            usuario.TarefasUsuario.Remove(tarefaUsuario);
            tarefa.UsuariosTarefa.Remove(tarefaUsuario);
        }

        public async Task RemoverUsuarioTarefas(int idUsuario)
        {
            var usuario = await _usuarioRepositorio.ObterUsuario(idUsuario);

            if (usuario == null)
            {
                _log.InfoFormat(BusinessMessages.RegistroNaoEncontrado, string.Format("usuario: {0}", idUsuario));
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, string.Format("usuario: {0}", idUsuario)));
            }

            var tarefasUsuario = usuario.TarefasUsuario.Where(e => e.IdUsuario == idUsuario).ToList();

            tarefasUsuario.Clear();
        }
    }
}
