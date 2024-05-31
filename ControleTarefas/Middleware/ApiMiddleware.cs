using ControleTarefas.API;
using ControleTarefas.Repositorio.Interface;
using ControleTarefas.Utilitarios;
using ControleTarefas.Utilitarios.Exceptions;
using ControleTarefas.Utilitarios.Messages;
using ControleTarefas.Utilitarios.Responses;
using log4net;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace ControleTarefas.Api.Middleware
{
    public class ApiMiddleware : IMiddleware
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ApiMiddleware));
        private readonly IGerenciadorTransacao _gerenciadorTransacao;

        public ApiMiddleware(IGerenciadorTransacao gerenciadorTransacao)
        {
            _gerenciadorTransacao = gerenciadorTransacao;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var transacaoObrigatoria = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<TransacaoObrigatoriaAttribute>();

            try
            {

                if (transacaoObrigatoria != null)
                {
                    await _gerenciadorTransacao.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);

                    await next.Invoke(context);

                    await _gerenciadorTransacao.CommitTransactionAsync();
                }
                else
                {
                    await next.Invoke(context);
                }


                stopwatch.Stop();
                _log.InfoFormat("Serviço executado com sucesso: {0} {1} [{2} ms]", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                if (transacaoObrigatoria != null)
                {
                    await _gerenciadorTransacao.RollbackTransactionsAsync();
                }

                stopwatch.Stop();
                _log.Error($"Erro no serviço: {context.Request.Path} / Mensagem: {ex.Message} [{stopwatch.ElapsedMilliseconds}]", ex);
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            var messages = new List<string>();

            switch (ex)
            {
                case BusinessException:
                    messages.Add(ex.Message);
                    break;
                case BusinessListException:
                    messages = ((BusinessListException)ex).Messages;
                    break;
                default:
                    messages.Add(string.Format(InfraMessages.ErroInesperado));
                    break;
            }

            await response.WriteAsync(JsonConvert.SerializeObject(new DefaultResponse(HttpStatusCode.InternalServerError, messages)));
        }
    }
}
