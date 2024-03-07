// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Tarefas.AMQP.Servicos;
using Tarefas.Salvar.DataBase.Repositorios;
using Tarefas.Salvar.DataBase.Services;

var hostBuilder = new HostBuilder()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddScoped<IRabbitMQService, RabbitMQService>();
            services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
            services.AddHostedService<ReceptorRabbitMQ>();
        });

hostBuilder.ConfigureLogging((hostingContext, logging) =>
{
    
    logging.AddSerilog(new LoggerConfiguration()
        // .ReadFrom.Configuration(hostingContext.Configuration)
        .WriteTo.File("logs/DataBase.log")
        .CreateLogger());
});

await hostBuilder.RunConsoleAsync();