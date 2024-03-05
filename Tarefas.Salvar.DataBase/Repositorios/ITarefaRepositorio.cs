using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas.Model.Models;

namespace Tarefas.Salvar.DataBase.Repositorios
{
    public interface ITarefaRepositorio
    {
        void SalvarTarefa(Tarefa tarefa);
        List<Tarefa> BuscaTarefas();
        Tarefa BuscaTarefa(long id);
    }
}
