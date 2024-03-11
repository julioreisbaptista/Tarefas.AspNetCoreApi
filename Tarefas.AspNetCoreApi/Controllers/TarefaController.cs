using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Tarefas.AMQP.Servicos;
using Tarefas.AspNetCoreApi.Services;
using Tarefas.Model.Models;
using Tarefas.Salvar.DataBase.Repositorios;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tarefas.AspNetCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly IRabbitMQService _factoryRabbit;
        private readonly ITarefaRepositorio _tarefaRepositorio;
        private readonly ILogger<TarefaController> _logger;

        public TarefaController(IRabbitMQService factoryRabbit,
            ITarefaRepositorio tarefaRepositorio,
            ILogger<TarefaController> logger)
        {
            _factoryRabbit = factoryRabbit;
            _tarefaRepositorio = tarefaRepositorio;
            _logger = logger;
        }

        [HttpPost(Name = "Salvar")]
        public ActionResult Post(Tarefa tarefa)
        {

            var responseObject = JsonConvert.SerializeObject(tarefa);

            _logger.LogInformation("Recebendo requisi��o para salvar tarefa");
            _logger.LogInformation($"Request: {0}", JsonConvert.SerializeObject(tarefa));


            if (_factoryRabbit.SendMessage(tarefa))
            {
                _logger.LogInformation("Tarefa enviada com sucesso.");
                return Ok("Tarefa enviada com sucesso.");
            }
            else
            {
                _logger.LogError("Falha ao enviar tarefa.");
                return BadRequest();
            }
        }

        [HttpGet(Name = "BuscaTodos")]
        public ActionResult<List<Tarefa>> Get()
        {
            _logger.LogInformation("Recebendo requisi��o para buscar todas as tarefas.");
         
            var tarefas = _tarefaRepositorio.BuscaTarefas();
            _logger.LogInformation($"Encontradas {tarefas.Count} tarefas.");
            return tarefas;
        }

    }
}