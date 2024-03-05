using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas.Model.Models;

namespace Tarefas.AMQP.Servicos
{
    public interface IRabbitMQService
    {
        bool SendMessage(Tarefa tarefa);
        void StartListening(Action<string> messageHandler);
        void Dispose();
    }
}
