using System;

namespace console_treinamento
{
    public abstract class APessoa : IType
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
    }
}
