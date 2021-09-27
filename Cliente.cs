using System;

namespace console_treinamento
{
    public class Cliente : IPessoa
    {
        public int Id {get;set;}
        public string Nome {get;set;}
        public string Telefone {get;set;}
    }
}
