using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace console_treinamento
{
    public class PessoaRepositorio
    {
        public static List<IPessoa> Todos()
        {
            var clientes = new List<IPessoa>();

            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                var sql = $"select * from pessoas limit 1000";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        var cli = new Cliente();
                        cli.Id = Convert.ToInt32(dr["id"]);
                        cli.Nome = dr["nome"].ToString();
                        cli.Telefone = dr["telefone"].ToString();

                        clientes.Add(cli);
                    }
                }

                connection.Close();
            }

            return clientes;
        }

        public void Salvar(IPessoa pessoa)
        {
            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                if (pessoa.Id == 0)
                {
                    var sql = $"insert into pessoas(nome, telefone)values(@nome, @telefone)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", pessoa.Nome);
                        command.Parameters.AddWithValue("@telefone", pessoa.Telefone);

                        pessoa.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                else
                {
                    var sql = $"update pessoas set nome = @nome, telefone = @telefone where id = @id";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", pessoa.Id);
                        command.Parameters.AddWithValue("@nome", pessoa.Nome);
                        command.Parameters.AddWithValue("@telefone", pessoa.Telefone);

                        pessoa.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                connection.Close();
            }
        }

        public void Delete(IPessoa pessoa)
        {
            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                var sql = $"delete from pessoas where id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", pessoa.Id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}