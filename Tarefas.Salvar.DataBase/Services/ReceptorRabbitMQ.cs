using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tarefas.AMQP.Servicos;
using Tarefas.Model.Models;
using Tarefas.Salvar.DataBase.Repositorios;

namespace Tarefas.Salvar.DataBase.Services
{
    public class ReceptorRabbitMQ : IHostedService
    {
        public const string QUEUE_NAME = "tarefas";
        public IRabbitMQService RabbitMQService { get; set; }
        public ITarefaRepositorio TarefaRepositorio { get; }

        public ReceptorRabbitMQ(IRabbitMQService rabbitMQService, ITarefaRepositorio tarefaRepositorio)
        {
            RabbitMQService = rabbitMQService;
            TarefaRepositorio = tarefaRepositorio;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Rodando");
            RabbitMQService.StartListening(message =>
            {
                Console.WriteLine($"Received message: {message}");

                var tarefa = JsonConvert.DeserializeObject<Tarefa>(message);

                if (tarefa != null)
                {
                    TarefaRepositorio.SalvarTarefa(tarefa);
                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Receptor RabbitMQ is stopping.");
            return Task.CompletedTask;
        }
    }
}
