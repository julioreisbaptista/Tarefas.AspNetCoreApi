// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
await hostBuilder.RunConsoleAsync();