using Dapper;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas.Model.Models;

namespace Tarefas.Salvar.DataBase.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        public static MySqlConnection GetSqlConnection()
        {
            return new MySqlConnection("server=127.0.0.1;uid=root;pwd=master;database=tarefas");
        }
        

        public Tarefa BuscaTarefa(long id)
        {
            // Execute uma consulta SQL para selecionar o registro com o id desejado.
            using (var connection = GetSqlConnection())
            {
                string sql = "SELECT * FROM tarefas WHERE Id = @Id";

                // Crie um objeto DynamicParameters e defina o parâmetro @Id com o valor desejado.
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id); // Substitua 1 pelo id desejado.

                // Execute a consulta SQL e armazene o resultado em uma variável.
                return connection.QueryFirstOrDefault(sql, parameters);

            }
        }

        public List<Tarefa> BuscaTarefas()
        {
            try
            {
                using (MySqlConnection connection = GetSqlConnection())
                {
                    connection.Open();

                    var query = "SELECT * FROM tarefas";

                    return connection.Query<Tarefa>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar os dados!");
                return new List<Tarefa>();
            }
            
        }

        public void SalvarTarefa(Tarefa tarefa)
        {
            try
            {
                if (tarefa != null && tarefa.Id != 0)
                {
                    using (MySqlConnection connection = GetSqlConnection())
                    {
                        connection.Open();

                        var query = "UPDATE tarefa SET Descricao = @Descricao, Data = @Data, Status = @Status WHERE Id = @Id";

                        connection.Execute(query, tarefa);
                    }
                }
                else
                {
                    using (MySqlConnection connection = GetSqlConnection())
                    {
                        connection.Open();

                        var query = "INSERT INTO tarefas (Descricao, Data, Status) VALUES (@Descricao, @Data, @Status)";

                        connection.Execute(query, tarefa);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao inserir ou atualizar a tarefa {tarefa.Id}");
            }
           
        }
    }
}
