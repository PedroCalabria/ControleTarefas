using Microsoft.AspNetCore;
using ControleTarefas.WebApi;
using log4net;
using System.Reflection;
using log4net.Config;

namespace ControleTarefas.API
{
    public class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetCallingAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                _log.Info("Iniciando a API");

                var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

                webHost.Build().Run();
            }
            catch (Exception ex)
            {
                _log.Fatal("Erro fatal", ex);
                throw;
            }
        }
    }
}