using System;

namespace console_treinamento
{
    public abstract class APessoa : IPessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Documento { get; set; }

        public abstract void SetDocumento(string doc);
    }
}
