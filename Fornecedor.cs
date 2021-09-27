using System;

namespace console_treinamento
{
    public class Fornecedor: APessoa
    {
        public string CNPJ
        {
            get
            {
                return this.Documento;
            }
            set
            {
                this.Documento = value;
            }
        }

        public override void SetDocumento(string cnpj)
        {
            this.Documento = cnpj;
        }
    }
}
