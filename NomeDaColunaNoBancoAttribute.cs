using System;

namespace console_treinamento
{
    internal class NomeDaColunaNoBancoAttribute : Attribute
    {
        public string Nome { get; set; }
        public bool NaoMapeada { get; set; }
        public bool IsPk { get; set; }
        public int Tamanho { get; set; }
        public string TipoNoBanco { get; set; }
    }
}