using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NPOI.SS.Formula.Functions;
using Tarefas.AMQP.Servicos;
using Tarefas.AspNetCoreApi.Controllers;
using Tarefas.Model.Models;
using Tarefas.Salvar.DataBase.Repositorios;


namespace TestTarefas
{
    [TestClass]
    public class RepositorioTest
    {
       
        
        [TestMethod]
        public void Get_ReturnsAllTarefas()
        {
            // Arrange

            Mock<ITarefaRepositorio> tarefaRepositorio = new Mock<ITarefaRepositorio>();
            Mock<ILogger<TarefaRepositorio>> mockLogger = new Mock<ILogger<TarefaRepositorio>>();
            TarefaRepositorio rs = new TarefaRepositorio(mockLogger.Object);

            // Act
            IEnumerable<Tarefa> result = rs.BuscaTarefas() ;

            // Assert
            Assert.AreNotEqual( 0 , result.Count());
           
        }


        [TestMethod]
        public void TestSalvarTarefa_NewTask()
        {
            // Arrange
            var tarefa = new Tarefa { Descricao = "Nova Tarefa", Data = new DateTime(2022, 1, 1), Status = 1 };
            Mock<ITarefaRepositorio> tarefaRepositorio = new Mock<ITarefaRepositorio>();
            Mock<ILogger<TarefaRepositorio>> mockLogger = new Mock<ILogger<TarefaRepositorio>>();
            TarefaRepositorio rs = new TarefaRepositorio(mockLogger.Object);

            // Act
            rs.SalvarTarefa(tarefa);
            Tarefa result = rs.BuscaTarefa(tarefa);

            // Assert
            Assert.AreEqual(tarefa.Descricao, result.Descricao);
            Assert.AreEqual(tarefa.Data, result.Data);
            Assert.AreEqual(tarefa.Status, result.Status);

        }

        [TestMethod]
        public void TestSalvarTarefa_ExistingTask()
        {
            // Arrange
            var existingTarefa = new Tarefa { Descricao = "Tarefa Existente", Data = new DateTime(2022, 1, 1), Status = 1 };
            Mock<ILogger<TarefaRepositorio>> mockLogger = new Mock<ILogger<TarefaRepositorio>>();
            TarefaRepositorio rs = new TarefaRepositorio(mockLogger.Object);

            // Act
            rs.SalvarTarefa(existingTarefa);
            Tarefa result = rs.BuscaTarefa(existingTarefa);
            // Assert
            Assert.AreEqual(existingTarefa.Descricao, result.Descricao);
            Assert.AreEqual(existingTarefa.Data, result.Data);
            Assert.AreEqual(existingTarefa.Status, result.Status);
        }

        [TestMethod]
        public void TestSalvarTarefa_NullInput()
        {
            // Arrange
            Tarefa tarefa = null;
            Mock<ILogger<TarefaRepositorio>> mockLogger = new Mock<ILogger<TarefaRepositorio>>();
            TarefaRepositorio rs = new TarefaRepositorio(mockLogger.Object);

            // Act
            rs.SalvarTarefa(tarefa);
            Tarefa result = rs.BuscaTarefa(tarefa);
            // Assert
            Assert.IsNull(result);
           
        }

        
    }
}