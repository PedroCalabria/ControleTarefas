using Microsoft.AspNetCore;
using ControleTarefas.WebApi;

var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

webHost.Build().Run();