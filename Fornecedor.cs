using System;

namespace console_treinamento
{
    [Tabela("fornecedores")]
    public class Fornecedor: APessoa
    {
        public string CNPJ { get; set; }
    }
}
