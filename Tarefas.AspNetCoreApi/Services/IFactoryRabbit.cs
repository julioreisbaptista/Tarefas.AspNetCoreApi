using Tarefas.Model.Models;

namespace Tarefas.AspNetCoreApi.Services
{
    public interface IFactoryRabbit
    {
        bool PublicMessage(Tarefa tarefa);
    }
}
