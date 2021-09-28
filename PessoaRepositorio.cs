using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace console_treinamento
{
    public class PessoaRepositorio
    {
        public static List<T> Todos<T>()
        {
            var pessoas = new List<T>();

            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                var type = typeof(T);
                var sql = $"select * from {getTableName(type)} limit 1000";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        var pes = (T)Activator.CreateInstance(type);
                        
                        foreach (var pi in pes.GetType().GetProperties())
                        {
                            var notPersistedField = pi.GetCustomAttribute<NaoMapeadaAttribute>();
                            if (notPersistedField != null) continue;

                            var nomeColuna = pi.Name;

                            var nomeColunaAtributo = pi.GetCustomAttribute<NomeDaColunaNoBancoAttribute>();
                            if (nomeColunaAtributo != null) nomeColuna = nomeColunaAtributo.Nome;

                            if (dr[nomeColuna] != DBNull.Value)
                                pi.SetValue(pes, dr[nomeColuna]);
                        }

                        pessoas.Add(pes);
                    }
                }

                connection.Close();
            }

            return pessoas;
        }

        public static void Salvar<T>(T type)
        {
            IType iType = (IType)type;
            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                if (iType.Id == 0)
                {
                    var listColunas = new List<string>();
                    var parametters = new List<MySqlParameter>();
                    foreach (var pi in iType.GetType().GetProperties())
                    {
                        var notPersistedField = pi.GetCustomAttribute<NaoMapeadaAttribute>();
                        if (notPersistedField != null) continue;

                        var nomeColuna = pi.Name;

                        var nomeColunaAtributo = pi.GetCustomAttribute<NomeDaColunaNoBancoAttribute>();
                        if (nomeColunaAtributo != null) nomeColuna = nomeColunaAtributo.Nome;

                        listColunas.Add(nomeColuna);
                        parametters.Add(new MySqlParameter("@" + nomeColuna, pi.GetValue(iType)));
                    }

                    var colunasBase = string.Join(",", listColunas.ToArray());
                    var paramBase = string.Join(",", listColunas.Select( c => $"@{c}").ToList().ToArray());
                    var sql = $"insert into {getTableName(iType.GetType())}({colunasBase})values({paramBase})";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddRange(parametters.ToArray());
                        iType.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                else
                {
                    var listColunas = new List<string>();
                    var parametters = new List<MySqlParameter>();
                    foreach (var pi in iType.GetType().GetProperties())
                    {
                        var notPersistedField = pi.GetCustomAttribute<NaoMapeadaAttribute>();
                        if (notPersistedField != null) continue;
                        if (pi.Name == "Id") continue;

                        var nomeColuna = pi.Name;

                        var nomeColunaAtributo = pi.GetCustomAttribute<NomeDaColunaNoBancoAttribute>();
                        if (nomeColunaAtributo != null) nomeColuna = nomeColunaAtributo.Nome;

                        listColunas.Add($"{nomeColuna} = @{nomeColuna}");
                        parametters.Add(new MySqlParameter("@" + nomeColuna, pi.GetValue(iType)));
                    }
                    parametters.Add(new MySqlParameter("@id", iType.Id));

                    var colunasBase = string.Join(",", listColunas.ToArray());

                    var sql = $"update pessoas set {colunasBase} where id = @id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        

                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }

        private static string getTableName(Type type)
        {
            var nomeTabela = $"{type.Name.ToLower()}s";
            var tableAttr = type.GetCustomAttribute<TabelaAttribute>();
            if (tableAttr != null) nomeTabela = tableAttr.Nome;

            return nomeTabela;
        }

        public static void Delete<T>(T type)
        {
            var iType = (IType)type;
            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                var sql = $"delete from pessoas where id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", iType.Id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
