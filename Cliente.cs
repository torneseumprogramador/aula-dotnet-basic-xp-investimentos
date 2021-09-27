﻿using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace console_treinamento
{
    public class Cliente : IFisica
    {
        public int Id {get;set;}
        public string Nome {get;set;}
        public string Telefone {get;set;}

        public static List<Cliente> Todos()
        {
            var clientes = new List<Cliente>();

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

        public void Salvar()
        {
            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                if (this.Id == 0)
                {
                    var sql = $"insert into pessoas(nome, telefone)values(@nome, @telefone)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", this.Nome);
                        command.Parameters.AddWithValue("@telefone", this.Telefone);

                        this.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                else
                {
                    var sql = $"update pessoas set nome = @nome, telefone = @telefone where id = @id";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", this.Id);
                        command.Parameters.AddWithValue("@nome", this.Nome);
                        command.Parameters.AddWithValue("@telefone", this.Telefone);

                        this.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                connection.Close();
            }
        }

        public static void DeletePorId(int id)
        {
            new Cliente { Id = id }.Delete();
        }

        public void Delete()
        {
            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                var sql = $"delete from pessoas where id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", this.Id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
