using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Tarefas.Model.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

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

        /// <summary>
        /// Retrieves a specific task from the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the task to retrieve.</param>
        /// <returns>The task with the specified id, or null if not found.</returns>
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


        /// <summary>
        /// Retrieves a specific task from the database based on the given id.
        /// </summary>
        /// <param name="Tarefa">The object tarefa of the task to retrieve.</param>
        /// <returns>The task with the specified id, or null if not found.</returns>
        public Tarefa BuscaTarefa(Tarefa tarefa)
        {
            try
            {
                using (MySqlConnection connection = GetSqlConnection(connectionString))
                {
                    connection.Open();
                    _logger.LogInformation("BuscaTarefa Chamado");

                    var query = "SELECT * FROM tarefas " +
                        "WHERE  Descricao = @Descricao and Data = @Data and @Status = Status ";

                    var ret = connection.QueryFirstOrDefault<Tarefa>(query, tarefa);

                    return ret;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar a tarefa");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all tasks from the database.
        /// </summary>
        /// <returns>A list of all tasks in the database.</returns>
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


        /// <summary>
        /// Retrieves all tasks from the database.
        /// </summary>
        /// <returns>A list of all tasks in the database.</returns>
        public List<Tarefa> BuscaTarefas(Tarefa tarefa)
        {
            try
            {
                using (MySqlConnection connection = GetSqlConnection(connectionString))
                {
                    connection.Open();
                    _logger.LogInformation("BuscaTarefas Chamado");

                    var query = "SELECT * FROM tarefas  "
                         +
                        "WHERE  Descricao = @Descricao and Data = @Data and @Status = Status ";
                    ;
                    return connection.Query<Tarefa>(query,tarefa).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar os dados");
                return new List<Tarefa>();
            }
        }

        /// <summary>
        /// Saves a task to the database.
        /// </summary>
        /// <param name="tarefa">The task to be saved.</param>
        public void SalvarTarefa(Tarefa tarefa)
        {
            try
            {
                _logger.LogInformation("SalvarTarefa Chamado");
                if (tarefa != null)
                    using (MySqlConnection connection = GetSqlConnection(connectionString))
                    {
                        connection.Open();

                        var ret = connection.QueryFirstOrDefault<Tarefa>("SELECT * FROM tarefas where Descricao = @Descricao and Data = @Data", tarefa);

                        if (ret != null)
                        {
                            tarefa.Id = ret.Id;
                            connection.Execute("UPDATE tarefas SET Descricao = @Descricao, Data = @Data, Status = @Status where Id = @Id", tarefa);
                        }
                        else
                        {
                            connection.Execute("INSERT INTO tarefas (Descricao, Data, Status) VALUES (@Descricao, @Data, @Status)", tarefa);
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
