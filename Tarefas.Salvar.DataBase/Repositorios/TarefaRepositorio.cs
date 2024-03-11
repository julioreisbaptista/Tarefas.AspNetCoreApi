using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Tarefas.Model.Models;
using Microsoft.Extensions.Configuration;

namespace Tarefas.Salvar.DataBase.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly ILogger<TarefaRepositorio> _logger;
        private string connectionString = "";

        public TarefaRepositorio(ILogger<TarefaRepositorio> logger)
        {
            _logger = logger;

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public static MySqlConnection GetSqlConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        public Tarefa BuscaTarefa(int id)
        {
            try
            {
                using (MySqlConnection connection = GetSqlConnection(connectionString))
                {
                    connection.Open();
                    _logger.LogInformation("BuscaTarefa Chamado");

                    var query = "SELECT * FROM tarefas WHERE id = @Id";
                    return connection.QueryFirstOrDefault<Tarefa>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar a tarefa");
                return null;
            }
        }


        public List<Tarefa> BuscaTarefas()
        {
            try
            {
                using (MySqlConnection connection = GetSqlConnection(connectionString))
                {
                    connection.Open();
                    _logger.LogInformation("BuscaTarefas Chamado");

                    var query = "SELECT * FROM tarefas";
                    return connection.Query<Tarefa>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar os dados");
                return new List<Tarefa>();
            }
        }


        public void SalvarTarefa(Tarefa tarefa)
        {
            try
            {
                _logger.LogInformation("SalvarTarefa Chamado");

                if (tarefa != null && tarefa.Id != 0)
                {
                    using (MySqlConnection connection = GetSqlConnection(connectionString))
                    {
                        connection.Open();

                        var query = "UPDATE tarefa SET Descricao = @Descricao, Data = @Data, Status = @Status WHERE Id = @Id";

                        connection.Execute(query, tarefa);
                    }
                }
                else
                {
                    using (MySqlConnection connection = GetSqlConnection(connectionString))
                    {
                        connection.Open();

                        var query = "INSERT INTO tarefas (Descricao, Data, Status) VALUES (@Descricao, @Data, @Status)";

                        connection.Execute(query, tarefa);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar a tarefa");
                Console.WriteLine($"Erro ao inserir ou atualizar a tarefa {tarefa.Id}");
            }

        }

    }
}
