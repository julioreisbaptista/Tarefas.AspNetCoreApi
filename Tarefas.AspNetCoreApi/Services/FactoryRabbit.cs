using RabbitMQ.Client;
using System.Text.Json.Serialization;
using Tarefas.Model.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;

namespace Tarefas.AspNetCoreApi.Services
{
    public class FactoryRabbit : IFactoryRabbit
    {
        private readonly ConnectionFactory _factory;
        public const string QUEUE_NAME = "tarefas";

        public FactoryRabbit()
        {
            _factory = FactoryModel.CreateModel();
        }

        public bool PublicMessage(Tarefa tarefa)
        {
            if (tarefa is null)
                return false;

            try
            {
                using (var connection = _factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                                queue: QUEUE_NAME,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null
                            );

                        var stringfiedMessage = JsonConvert.SerializeObject(tarefa);
                        var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

                        channel.BasicPublish(
                                exchange: "",
                                routingKey: QUEUE_NAME,
                                basicProperties: null,
                                body: bytesMessage
                            );

                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                string message = $"Erro de conexão ao conectar no RabbitMQ {ex.GetBaseException().Message}";
                return false;
            }
            
        }
    }
}
