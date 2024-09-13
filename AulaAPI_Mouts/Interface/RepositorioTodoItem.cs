using AulaAPI_Mouts.Models;
using System.Data.SqlClient;
using AulaAPI_Mouts.Interface;
using System.Collections.Generic;
using System.Linq;
using AulaAPI_Mouts.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AulaAPI_Mouts.Interface
{
    public class RepositorioTodoItem : IRepositorio<TodoItem, int>
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;InitialCatalog=ToDo;Integrated Security = True";

        public bool Alterar(int id, TodoItem item)
        {
            string updateQuery = "UPDATE TodoItem SET Name = @Name, IsComplete = @IsComplete, Secret = @Secret WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@IsComplete", item.IsComplete);
                command.Parameters.AddWithValue("@Secret", item.Secret);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return false;
        }

        public IEnumerable<TodoItem> Consultar()
        {
            List<TodoItem> TodoItems = new List<TodoItem>();
            string selectQuery = "SELECT Id, Name, IsComplete, Secret FROM TodoItem";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TodoItems.Add(new TodoItem
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                IsComplete = reader.GetBoolean(reader.GetOrdinal("IsComplete")),
                                Secret = reader.GetString(reader.GetOrdinal("Secret"))
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return TodoItems;
        }

        public TodoItem Consultar(int id)
        {
            string selectQuery = "SELECT Id, Name, IsComplete, Secret FROM TodoItem WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TodoItem
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                IsComplete = reader.GetBoolean(reader.GetOrdinal("IsComplete")),
                                Secret = reader.GetString(reader.GetOrdinal("Secret"))
                            };
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return null;
        }

        public void Excluir(TodoItem item)
        {
            string deleteQuery = "DELETE FROM TodoItem WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", item.Id);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Tarefa excluída com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }

        public TodoItem Salvar(TodoItem item)
        {
            string insertQuery = "INSERT INTO TodoItem (Name, IsComplete, Secret) VALUES (@Name, @IsComplete, @Secret); SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@IsComplete", item.IsComplete);
                command.Parameters.AddWithValue("@Secret", item.Secret);
                try

                {
                    connection.Open();
                    item.Id = Convert.ToInt32(command.ExecuteScalar());
                    return item;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return null;
        }
    }
}
