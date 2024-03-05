using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Tarefas.AMQP.Servicos;
using Tarefas.AspNetCoreApi.Services;
using Tarefas.Model.Models;
using Tarefas.Salvar.DataBase.Repositorios;

namespace Tarefas.AspNetCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly IRabbitMQService _factoryRabbit;
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(IRabbitMQService factoryRabbit, 
            ITarefaRepositorio tarefaRepositorio)
        {
            _factoryRabbit = factoryRabbit;
            _tarefaRepositorio = tarefaRepositorio;
        }

        [HttpPost(Name = "Salvar")]
        public ActionResult Post(Tarefa tarefa)
        {
            if (_factoryRabbit.SendMessage(tarefa))
                return Ok("Tarefa enviada com sucesso.");
            else
                return BadRequest();
        }


        [HttpGet(Name = "BuscaTodos")]
        public ActionResult<List<Tarefa>> Get()
        {
            return _tarefaRepositorio.BuscaTarefas();
        }


    }
}