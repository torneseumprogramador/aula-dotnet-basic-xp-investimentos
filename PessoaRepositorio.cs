using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace console_treinamento
{
    public class PessoaRepositorio
    {
        public PessoaRepositorio(Type type)
        {
            this.type = type;
        }

        private Type type;

        public List<IPessoa> Todos()
        {
            var pessoas = new List<IPessoa>();

            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                var sql = $"select * from pessoas where tipo = @tipo limit 1000";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tipo", type.Name);

                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        var pes = (IPessoa)Activator.CreateInstance(type);
                        pes.Id = Convert.ToInt32(dr["id"]);
                        pes.Nome = dr["nome"].ToString();
                        pes.Telefone = dr["telefone"].ToString();
                        pes.Documento = dr["documento"].ToString();

                        // Reflaction para amanhã
                        //EnderecoDoCliente

                        pessoas.Add(pes);
                    }
                }

                connection.Close();
            }

            return pessoas;
        }

        public void Salvar(IPessoa pessoa)
        {
            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                if (pessoa.Id == 0)
                {
                    var sql = $"insert into pessoas(nome, telefone, tipo, documento)values(@nome, @telefone, @tipo, @documento)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", pessoa.Nome);
                        command.Parameters.AddWithValue("@telefone", pessoa.Telefone);
                        command.Parameters.AddWithValue("@tipo", pessoa.GetType().Name);
                        command.Parameters.AddWithValue("@documento", pessoa.Documento);


                        pessoa.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                else
                {
                    var sql = $"update pessoas set nome = @nome, telefone = @telefone, tipo = @tipo, documento = @documento where id = @id";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", pessoa.Id);
                        command.Parameters.AddWithValue("@nome", pessoa.Nome);
                        command.Parameters.AddWithValue("@telefone", pessoa.Telefone);
                        command.Parameters.AddWithValue("@tipo", pessoa.GetType().Name);
                        command.Parameters.AddWithValue("@documento", pessoa.Documento);

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
