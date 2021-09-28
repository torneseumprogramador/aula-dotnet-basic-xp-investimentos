using System;

namespace console_treinamento
{
    internal class TabelaAttribute : Attribute
    {
        public TabelaAttribute(string nome)
        {
            this.Nome = nome;
        }

        public string Nome { get; set; }
    }
}