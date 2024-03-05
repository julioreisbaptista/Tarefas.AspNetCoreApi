namespace Tarefas.Model.Models
{
    public class Tarefa
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int Status { get; set; }
    }
}