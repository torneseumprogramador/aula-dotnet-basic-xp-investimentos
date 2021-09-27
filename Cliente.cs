using System;
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
    }
}
