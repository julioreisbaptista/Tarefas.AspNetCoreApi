using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarefas.Model.Models
{
    public class FactoryModel
    {
        public static ConnectionFactory _factoryModel = new ConnectionFactory();
        public static ConnectionFactory CreateModel()
        {
            if (_factoryModel == null)
                return new ConnectionFactory()
                {
                    HostName = "localhost",
                    //UserName = "guest",
                    //Password = "guest",
                    //Port = 5672
                };

            return _factoryModel;
        }
    }
}
