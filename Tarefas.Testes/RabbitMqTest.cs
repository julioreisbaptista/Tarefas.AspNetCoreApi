using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas.AMQP.Servicos;
using Tarefas.AspNetCoreApi.Controllers;
using Tarefas.Model.Models;
using Tarefas.Salvar.DataBase.Repositorios;
using Newtonsoft.Json;

namespace TestTarefas
{
    [TestClass]
    public class RabbitMqTest
    {
        [TestMethod]
        public void SendMessage_ValidTarefa_ReturnsTrue()
        {
            // Arrange
            Tarefa tarefa = new Tarefa()
            {
                Data = new DateTime(2022, 12, 31),
                Descricao = "Tarefa de teste",
                Status = 1
            };
            RabbitMQService rabbitMQService = new RabbitMQService();

            // Act
            bool result = rabbitMQService.SendMessage(tarefa);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SendMessage_ExceptionThrown_ReturnsFalse()
        {
            // Arrange
            Tarefa tarefa = new Tarefa();
            RabbitMQService rabbitMQService = new RabbitMQService();

            // Act
            bool result = rabbitMQService.SendMessage(tarefa);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetMessage_Returns_WhenBasicGetReturns()
        {
            // Arrange
            Tarefa tarefa = new Tarefa
            {
                Data = new DateTime(2022, 12, 31),
                Descricao = "Tarefa de teste de leitura",
                Status = 1
            };
            RabbitMQService rabbitMQService = new RabbitMQService();
            
            // Act
            bool result = rabbitMQService.SendMessage(tarefa);
            string message = rabbitMQService.GetMessage();
            Tarefa retTarefa = JsonConvert.DeserializeObject<Tarefa>(message);

            // Assert
            Assert.IsNotNull(retTarefa);
            Assert.IsTrue(result);
            Assert.AreEqual(tarefa.Data, retTarefa.Data);
            Assert.AreEqual(tarefa.Descricao, retTarefa.Descricao);
            Assert.AreEqual(tarefa.Status, retTarefa.Status);
        }
    }
}